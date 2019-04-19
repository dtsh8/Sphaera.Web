using System.Threading.Tasks;
﻿using System.Linq;
﻿using System.Collections.Generic;
using System.Web;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Extensions;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Models.Assignment;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IFileService
    {
        Task<FileWithDescription[]> GetList(string incidentId, string cardId);

        Task<byte[]> Get(string incidentId, string cardId, string fileName);

        Task<bool> Update(string incidentId, string cardId, string fileName, string description, byte[] fileBody);

        Task<bool> Delete(string incidentId, string cardId, string fileName);
    }

    [UsedImplicitly]
    public class FileService : SeviceBaseSimple, IFileService
    {
        private readonly string _idmUrl;

        #region Private Consts

        private const string GetFileListUri = "/api/v1/File/Get?incidentId={0}&cardId={1}";
        private const string GetFileUri = "/api/v1/File/GetCardFile";
        private const string PutFileUri = "/api/v1/File/Put?incidentId={0}&cardId={1}&fileName={2}";
        private const string DelFileUri = "/api/v1/File/Delete?incidentId={0}&cardId={1}&fileName={2}";

        #endregion

        #region Constructor

        public FileService([NotNull] IConfiguration config)
        {
            SvcUrl = config["FileSvc_URI"];
            _idmUrl = config["IdmInternalBaseAddress"];
        }

        #endregion

        #region Public Methods

        public async Task<FileWithDescription[]> GetList(string incidentId, string cardId)
        {
            var fileNamesTask = base.GetList<string>(string.Format(GetFileListUri, incidentId, cardId));
            
            var idmProxy = new WebApiProxy(_idmUrl, true);
            var propertiesSetsUrl = $"api/AssignmentFilePropertiesSet/GetList?incidentId={incidentId}&cardId={cardId}";
            var fileDescriptionTasks = idmProxy.GetResultAsync<IEnumerable<AssignmentFilePropertiesSetModel>>(propertiesSetsUrl);

            await Task.WhenAll(fileNamesTask, fileDescriptionTasks);
            var descriptions = fileDescriptionTasks.Result.ToDictionary(d => d.FileName, d => d.Description);

            return fileNamesTask.Result.Select(name =>
            {
                var description = descriptions.TryGetOrDefault(name) ?? "";
                return new FileWithDescription()
                {
                    Description = description,
                    FileName = name
                };
            }).ToArray();
        }

        public async Task<byte[]> Get(string incidentId, string cardId, string fileName)
        {
            var getParameters = HttpUtility.ParseQueryString("");
            getParameters["incidentId"] = incidentId;
            getParameters["cardId"] = cardId;
            getParameters["fileName"] = fileName;
            return await base.Get<byte[]>($"{GetFileUri}?{getParameters}");
        }

        public async Task<bool> Update(string incidentId, string cardId, string fileName, string description, byte[] fileBody)
        {
            var updated = await base.Update<byte[], bool>(string.Format(PutFileUri, incidentId, cardId, fileName), fileBody);
            if (!updated)
                return false;

            var idmProxy = new WebApiProxy(_idmUrl, true);
            await idmProxy.PutAsync("api/AssignmentFilePropertiesSet/Update", new AssignmentFilePropertiesSetModel()
            {
                IncidentId = incidentId,
                CardId = cardId,
                FileName = fileName,
                Description = description
            });

            return true;
        }

        public async Task<bool> Delete(string incidentId, string cardId, string fileName)
        {
            return await base.Delete<bool>(string.Format(DelFileUri, incidentId, cardId, fileName));
        }

        #endregion
    }
}