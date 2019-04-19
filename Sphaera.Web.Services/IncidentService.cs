using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Interfaces;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Models.Cards;

namespace Sphaera.Web.Services
{
    public interface IIncidentService
    {
        Task<Incident> Get(IncidentGetRequest request);

        Task<CreateIncidentResult> Update(Incident incident);

        Task<IncidentHistory[]> GetHistory(string incidentId);
    }

    [UsedImplicitly]
    public class IncidentService : IIncidentService
    {
        #region Private Consts

        private const string GetIncidentUri = "/api/v1/Incident/Get";
        private const string PutIncidentUri = "/api/v1/Incident/Put?mainServiceTypeId={0}";
        private const string GetHistoryUri = "/api/v1/Incident/GetHistory?incidentId={0}";

        private readonly WebApiProxy _webApiProxy;
        private readonly IServiceTypeIdService _serviceTypeIdService;
        private readonly Lazy<ILogger> _logger;

        #endregion

        #region Constructor

        public IncidentService([NotNull] IConfiguration config,
            IServiceTypeIdService serviceTypeIdService,
            ILoggerFactory loggerFactory)
        {
            _serviceTypeIdService = serviceTypeIdService;
            string incidentUri = config["Incident_URI"];
            _logger = new Lazy<ILogger>(loggerFactory.CreateLogger<IncidentService>);
            _webApiProxy = new WebApiProxy(incidentUri);
        }

        #endregion

        #region Public Methods

        public async Task<Incident> Get(IncidentGetRequest request)
        {
            var incident = await _webApiProxy.PostAsync<IncidentGetRequest, Incident>(GetIncidentUri, request);
            return incident;
        }

        public async Task<CreateIncidentResult> Update(Incident incident)
        {
            incident.Address.Highway = incident.Address.Highway ?? new Highway(); 

            var currentServiceTypeId = await _serviceTypeIdService.GetCurrentServiceTypeId();
            bool isIncidentCreation = string.IsNullOrEmpty(incident.IncidentId);
            if (isIncidentCreation)
            {
                Card card = incident.Cards.First();
                card.ServiceTypeId = currentServiceTypeId;
                card.Created = DateTime.UtcNow;
                card.StateId = 1;
                card.IsDanger = false;
                card.IsOverdue = false;
                incident.Claim.SmsSubscribe = false;
                incident.Claim.EmailSubscribe = false;
            }

            var serializedObject = JsonConvert.SerializeObject(incident);
            _logger.Value.LogInformation($"Incident.Update: {serializedObject}");
            string apiPath = string.Format(PutIncidentUri, currentServiceTypeId);
            string updatedIncidentId = await _webApiProxy.PutAsync<Incident, string>(apiPath, incident);
            _logger.Value.LogInformation($"Incident.Update: Получен IncidentId: {updatedIncidentId}.");
            var createIncidentResult = new CreateIncidentResult();
            createIncidentResult.IncidentId = updatedIncidentId;
            if (isIncidentCreation && !string.IsNullOrEmpty(updatedIncidentId))
            {
                // Получаем id созданной карточки.
                var createdIncident = await Get(new IncidentGetRequest {IncidentId = updatedIncidentId});
                Card card = incident.Cards.First();
                Card updatedCard = createdIncident.Cards.FirstOrDefault(t => t.ServiceTypeId == card.ServiceTypeId);
                if (updatedCard == null)
                {
                    throw new InvalidOperationException(
                        $"Не найдена карточка в инциденте ${createdIncident.IncidentId} с типом ${card.ServiceTypeId}");
                }

                createIncidentResult.CardId = updatedCard.CardId;
            }

            return createIncidentResult;
        }

        public async Task<IncidentHistory[]> GetHistory(string incidentId)
        {
            string apiPath = string.Format(GetHistoryUri, incidentId);
            var incidentHistories = await _webApiProxy.GetResultAsync<IncidentHistory[]>(apiPath);
            return incidentHistories;
        }

        #endregion
    }
}