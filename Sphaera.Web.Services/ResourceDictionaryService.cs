using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IResourceDictionaryService
    {
        [NotNull]
        Task<Resource[]> GetList(long serviceTypeId, [CanBeNull] string resourceTypeCode, [CanBeNull] string stationCode);

        [NotNull]
        Task<bool> Update([NotNull] Resource data);
    }

    public class ResourceDictionaryService : SeviceBaseSimple, IResourceDictionaryService
    {
        #region Private Consts

        private const string GetResourceList = "/api/v1/Resource/Get?serviceTypeId={0}&resourceTypeCode={1}&stationCode={2}";
        private const string PutResource = "/api/v1/Resource/Put";

        #endregion

        private readonly Lazy<ILogger> _logger;

        #region Constructor

        public ResourceDictionaryService([NotNull] IConfiguration config,
            ILoggerFactory loggerFactory)
        {
            SvcUrl = config["ResourceDictionarySvc_URI"];
            _logger = new Lazy<ILogger>(loggerFactory.CreateLogger<ResourceDictionaryService>);
        }

        #endregion

        #region Public Methods


        public async Task<Resource[]> GetList(long serviceTypeId, string resourceTypeCode, string stationCode)
        {
            return await base.GetList<Resource>(string.Format(GetResourceList, serviceTypeId, resourceTypeCode, stationCode));
        }

        public async Task<bool> Update(Resource obj)
        {
            _logger.Value.LogInformation($"Resource Update: {JsonConvert.SerializeObject(obj)}");
            return await base.Update<Resource, bool>(PutResource, obj);
        }

        #endregion
    }
}