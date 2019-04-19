using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sphaera.Web.Server.Extensions;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Models.Assignment;

namespace Sphaera.Web.Services
{
    /// <summary>
    /// Сервис поручений
    /// </summary>
    public interface IAssignmentService
    {
        /// <summary>
        /// Получить поручения (только открытые)
        /// </summary>
        /// <param name="userLogin">Логин руководителя</param>
        /// <param name="incidentId">Идентификатор происшествия</param>
        /// <param name="cardId">Идентификатор карточки поручения (опционально)</param>
        /// <returns>Список поручений</returns>
        Task<Assignment[]> Get(string userLogin, string incidentId, string cardId);

        /// <summary>
        /// Поручения для отчетов
        /// </summary>
        /// <param name="userLogin">Логин создателя</param>
        /// <param name="dateFrom">Дата от</param>
        /// <param name="dateTo">Дата до</param>
        /// <returns>Список поручений</returns>
        Task<Assignment[]> GetReport(string userLogin, DateTime? dateFrom, DateTime? dateTo);

        /// <summary>
        /// Получение списка файлов поручения
        /// </summary>
        /// <param name="incidentId">Идентификатор происшествия</param>
        /// <param name="cardId">Идентификатор карточки поручения</param>
        /// <returns>Список приложенных файлов поручения</returns>
        Task<AssignmentFile[]> GetAssignmentFiles(string incidentId, string cardId);

        /// <summary>
        /// Получение файла поручения
        /// </summary>
        /// <param name="incidentId">Идентификатор происшествия</param>
        /// <param name="cardId">Идентификатор карточки поручения</param>
        /// <param name="fileName">Имя приложенного файла</param>
        /// <returns>Тело приложенного файла</returns>
        Task<byte[]> GetAssignmentFile(string incidentId, string cardId, string fileName);

        /// <summary>
        /// Сохранение файла поручения
        /// </summary>
        /// <param name="incidentId">Идентификатор происшествия</param>
        /// <param name="cardId">Идентификатор карточки поручения</param>
        /// <param name="fileName">Имя приложенного файла</param>
        /// <param name="description">Описание файла</param>
        /// <param name="fileBody">Тело приложенного файла</param>
        /// <returns>Признак успешности операции</returns>
        Task<bool> SetAssignmentFile(string incidentId, string cardId, string fileName, string description, byte[] fileBody);

        /// <summary>
        /// Получение истории изменения поручения
        /// </summary>
        /// <param name="assignmentId">Идентификатор поручения</param>
        /// <returns>Список изменений статусов поручения</returns>
        Task<AssignmentHistory[]> GetHistory(long assignmentId);

        /// <summary>
        /// Получение шаблонов
        /// </summary>
        /// <returns>Список шаблонов поручений</returns>
        Task<AssignmentTemplate[]> GetTemplates();

        /// <summary>
        /// Обновить/Создать поручение
        /// </summary>
        Task<Assignment> Put(Assignment assignment);

        /// <summary>
        /// Изменить состояние поручения
        /// </summary>
        /// <param name="request">Запрос на изменение статуса поручения</param>
        /// <returns>Поручение</returns>
        Task<Assignment> SetAssignmentState(SetAssignmentStateRequest request);

        /// <summary>
        /// Отправить на доработку.
        /// </summary>
        /// <returns>Поручение</returns>
        Task<Assignment> DemandRevision(DemandRevisionRequest request);
    }

    [PublicAPI]
    public class AssignmentService : IAssignmentService
    {
        private const string GetAssignmentUri = "/api/v1/Assignment/Get?userLogin={0}&incidentId={1}&cardId={2}";
        private const string PutAssignmentUri = "/api/v1/Assignment/Put";
        private const string SetAssignmentStateUri = "/api/v1/Assignment/SetAssignmentState";
        private const string GetAssignmentFilesUri = "/api/v1/Assignment/GetAssignmentFiles?incidentId={0}&cardId={1}";
        private const string GetAssignmentFileUri = "/api/v1/Assignment/GetAssignmentFile?incidentId={0}&cardId={1}&fileName={2}";
        private const string SetAssignmentFileUri = "/api/v1/Assignment/SetAssignmentFile?incidentId={0}&cardId={1}&fileName={2}";
        private const string GetAssignmentHistoryUri = "/api/v1/Assignment/GetHistory?assignmentId={0}";
        private const string GetTemplatesUri = "/api/v1/Assignment/GetTemplates";
        private const string GetReportUri = "/api/v1/Assignment/GetReport";

        private readonly WebApiProxy _webApiProxy;
        private readonly WebApiProxy _idmApiProxy;

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IAssignmentStateService _assignmentStateService;

        private readonly Lazy<ILogger> _logger;

        public AssignmentService([NotNull] IConfiguration config,
            IHttpContextAccessor contextAccessor,
            IAssignmentStateService assignmentStateService,
            ILoggerFactory loggerFactory)
        {
            _assignmentStateService = assignmentStateService;
            _contextAccessor = contextAccessor;
            var assignmentUri = config["Assignment_URI"];
            _webApiProxy = new WebApiProxy(assignmentUri);
            var idmUri = config["IdmInternalBaseAddress"];
            _idmApiProxy = new WebApiProxy(idmUri, true);
            _logger = new Lazy<ILogger>(loggerFactory.CreateLogger<AssignmentService>);
        }

        public async Task<Assignment[]> Get(string userLogin, string incidentId, string cardId)
        {
            var apiPath = string.Format(GetAssignmentUri, userLogin, incidentId, cardId);
            var assignments = await _webApiProxy.GetResultAsync<Assignment[]>(apiPath);
            return assignments;
        }

        public async Task<Assignment[]> GetReport(string userLogin, DateTime? dateFrom, DateTime? dateTo)
        {
            var getParams = HttpUtility.ParseQueryString("");
            getParams["userLogin"] = userLogin;
            if (dateFrom.HasValue)
            {
                getParams["dateFrom"] = dateFrom.Value.ToString("O");
            }
            if (dateTo.HasValue)
            {
                getParams["dateTo"] = dateTo.Value.ToString("O");
            }

            return await _webApiProxy.GetResultAsync<Assignment[]>($"{GetReportUri}?{getParams}");
        }

        public async Task<AssignmentFile[]> GetAssignmentFiles(string incidentId, string cardId)
        {
            var apiPath = string.Format(GetAssignmentFilesUri, incidentId, cardId);
            var assignmentFilesNamesTask = _webApiProxy.GetResultAsync<string[]>(apiPath);

            var propertiesSetsUrl = $"api/AssignmentFilePropertiesSet/GetList?incidentId={incidentId}&cardId={cardId}";
            var assignmentFilesPropertiesSetsTask = _idmApiProxy.GetResultAsync<List<AssignmentFilePropertiesSetModel>>(propertiesSetsUrl);
            await Task.WhenAll(assignmentFilesPropertiesSetsTask, assignmentFilesNamesTask);
            var assignmentFilesPropertiesSets = assignmentFilesPropertiesSetsTask.Result.ToDictionary(fps => fps.FileName, fps => fps);
            var assignmentFilesNames = assignmentFilesNamesTask.Result;
            var assignmentFiles = assignmentFilesNames.Select(t => new AssignmentFile
                {FileName = t, Description = assignmentFilesPropertiesSets.TryGetOrDefault(t)?.Description ?? ""}).ToArray();
            return assignmentFiles;
        }

        public async Task<byte[]> GetAssignmentFile(string incidentId, string cardId, string fileName)
        {
            var apiPath = string.Format(GetAssignmentFileUri, incidentId, cardId, fileName);
            var fileBody = await _webApiProxy.GetResultAsync<byte[]>(apiPath);

            return fileBody;
        }

        public async Task<bool> SetAssignmentFile(
            string incidentId,
            string cardId,
            string fileName,
            string description,
            byte[] fileBody)
        {
            var apiPath = string.Format(SetAssignmentFileUri, incidentId, cardId, fileName);
            var success = await _webApiProxy.PutAsync<byte[], bool>(apiPath, fileBody);
            
            var propertiesSetsUrl = "api/AssignmentFilePropertiesSet/Update";
            await _idmApiProxy.PutAsync(propertiesSetsUrl, new AssignmentFilePropertiesSetModel()
            {
                IncidentId = incidentId,
                CardId = cardId,
                Description = description,
                FileName = fileName
            });
            
            return success;
        }

        public async Task<AssignmentHistory[]> GetHistory(long assignmentId)
        {
            var url = string.Format(GetAssignmentHistoryUri, assignmentId);
            return await _webApiProxy.GetResultAsync<AssignmentHistory[]>(url);
        }

        public async Task<AssignmentTemplate[]> GetTemplates()
        {
            return await _webApiProxy.GetResultAsync<AssignmentTemplate[]>(GetTemplatesUri);
        }

        public async Task<Assignment> Put(Assignment assignment)
        {
            bool isCreating = assignment.Id == 0;
            if (isCreating)
            {
                assignment.CreatedBy = _contextAccessor.GetUserName();
                var assignmentStates = await _assignmentStateService.GetList();
                const string draft = "DRAFT";
                var draftState = assignmentStates.FirstOrDefault(t => t.Code.Equals(draft, StringComparison.InvariantCultureIgnoreCase));
                if (draftState == null)
                {
                    throw new InvalidDataException($"Не найден статус поручения {draft} в справочнике.");
                }

                assignment.StateId = draftState.Id;
                assignment.StateCode = draftState.Code;
                assignment.State = draftState.Name;
                assignment.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            }

            var serializedObject = JsonConvert.SerializeObject(assignment);
            _logger.Value.LogInformation($"Assignment.Put: {serializedObject}");
            return await _webApiProxy.PutAsync(PutAssignmentUri, assignment);
        }

        public async Task<Assignment> SetAssignmentState(SetAssignmentStateRequest request)
        {
            return await _webApiProxy.PostAsync<SetAssignmentStateRequest, Assignment>(SetAssignmentStateUri, request);
        }

        public async Task<Assignment> DemandRevision(DemandRevisionRequest request)
        {
            var setAssignmentStateRequest = new SetAssignmentStateRequest()
            {
                Text = request.Text,
                AssignmentId = request.AssignmentId,
                ChangedBy = _contextAccessor.GetUserName(),
                DateChanged = DateTime.UtcNow,
                StateCode = "NEW" 
            };

            return await SetAssignmentState(setAssignmentStateRequest);
        }
    }
}