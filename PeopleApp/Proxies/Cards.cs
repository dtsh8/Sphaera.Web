using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Api.Configuration;
using Sphaera.Web.Api.Interfaces;
using Sphaera.Web.Api.Services;
using Sphaera.Web.Core;
using Sphaera.Web.Core.Cards;
using Sphaera.Web.Core.Enum;
using Sphaera.Web.PeoplePublicArea.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleApp.Proxies
{
    public interface ICards
    {
        Task<CardsListDto> GetCards(CardFilterDto cardFilterDto, string connectionId);

        /// <summary>
        /// Возвращает карточки обращений пользователя.
        /// </summary>
        Task<CardsListDto> GetAppealCards(CardFilterDto cardFilterDto, string userName, string connectionId);

        Task Refresh(IHubClients clients);

        void Unsubscribe(string connectionId);

        Task<CardDto> Insert(CardDto dto, string orgCode);

        Task<CardDto> Update(CardDto dto, string orgCode);

        Task Close(string incidentId, string cardId);

        Task Reject(string incidentId, string cardId, string message);

        Task<CardStatsticDto[]> GetStatistics(StatsticType statisticType, StatsticPeriodType periodType, int periodCount);

        /// <summary>
        /// Подписаться на чрезвычайные ситуации.
        /// </summary>
        /// <returns></returns>
        Task<string> SubscribeToEmergencyAlerts();

        /// <summary>
        /// Получить список чрезвычайных ситуаций.
        /// </summary>
        /// <returns></returns>
        Task RefreshEmergencyAlerts(string sessionId, IHubClients clients);
    }

    public class Cards : ICards
    {
        #region Private Consts

        private const string UpdateCardMethod = "updateCard";

        private const string UpdateAppealCardMethod = "updateAppealCard";

        private const string UpdateEmergencyCardMethod = "updateEmergencyCard";

        #endregion

        #region Private Fields

        private readonly ICardService _cardService;

        private readonly IMapper _mapper;

        private readonly IServiceTypeIdService _serviceTypeIdService;

        private readonly IConfiguration _configuration;

        private readonly IServiceTypeIdConfiguration _serviceTypeIdConfiguration;

        private readonly IIncidentStateService _incidentStateService;

        private readonly string _notAuthenticatedUserId = Guid.Empty.ToString();

        #endregion

        #region Constructor

        public Cards( ICardService cardService,
                      IMapper mapper,
                      IServiceTypeIdService serviceTypeIdService,
                      IConfiguration configuration,
                      IServiceTypeIdConfiguration serviceTypeIdConfiguration,
                      IIncidentStateService incidentStateService)
        {
            _incidentStateService = incidentStateService;
            _serviceTypeIdService = serviceTypeIdService;
            _configuration = configuration;
            _serviceTypeIdConfiguration = serviceTypeIdConfiguration;
            _cardService = cardService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        public async Task<CardsListDto> GetCards(CardFilterDto cardFilterDto, string connectionId)
        {
            cardFilterDto = cardFilterDto ?? new CardFilterDto();
            cardFilterDto.ServiceTypeIds = new List<long>
            {
                _serviceTypeIdConfiguration.SaveCityServiceTypeId
            };
            cardFilterDto.ExceptServiceTypeIds = false;
            IncidentState newState = await _incidentStateService.GetNew();
            IncidentState acceptedState = await _incidentStateService.GetAccepted();
            cardFilterDto.StateIds = new List<long> { newState.Id, acceptedState.Id };
            if (!cardFilterDto.CreatedFrom.HasValue)
            {
                cardFilterDto.CreatedFrom = DateTime.UtcNow.AddDays(-7);
            }

            cardFilterDto.CardIndexCodes = EmergencyAlertConfiguration.GetEmergencyAlertCardIndexCodes(_configuration);
            cardFilterDto.ExceptCardIndexCodes = true;

            return await GetCardsByFilter(cardFilterDto, CardJournalType.Accident, connectionId);
        }

        public async Task<CardsListDto> GetAppealCards(CardFilterDto cardFilterDto, string userName, string connectionId)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            cardFilterDto = cardFilterDto ?? new CardFilterDto();
            cardFilterDto.ServiceTypeIds = new List<long>
            {
                _serviceTypeIdConfiguration.PeoplePublicAreaServiceTypeId
            };
            cardFilterDto.ExceptServiceTypeIds = false;
            cardFilterDto.UserLogin = userName;

            return await GetCardsByFilter(cardFilterDto, CardJournalType.Appeal, connectionId);
        }

        public async Task Refresh(IHubClients clients)
        {
            await _cardService.Refresh(async (connectionId, cardJournalType, changes) =>
            {
                var newCardsList = _mapper.Map<CardsListDto>(changes);

                string clientMethod;
                switch (cardJournalType)
                {
                    case CardJournalType.Appeal:
                        clientMethod = UpdateAppealCardMethod;
                        break;
                    default:
                        clientMethod = UpdateCardMethod;
                        break;
                }

                await clients.Client(connectionId).SendAsync(clientMethod, newCardsList);
            });
        }

        public void Unsubscribe(string connectionId)
        {
            _cardService.Unsubscribe(connectionId);
        }

        public async Task<CardDto> Insert(CardDto dto, string orgCode)
        {
            var card = _mapper.Map<CardDto, Card>(dto);
            Card insertedCard = await _cardService.Update(card, orgCode);
            return _mapper.Map<CardDto>(insertedCard);
        }

        public async Task<CardDto> Update(CardDto dto, string orgCode)
        {
            var card = _mapper.Map<CardDto, Card>(dto);
            Card updatedCard = await _cardService.Update(card, orgCode);
            return _mapper.Map<CardDto>(updatedCard);
        }

        public async Task Close(string incidentId, string cardId)
        {
            await _cardService.Close(incidentId, cardId);
        }

        public async Task Reject(string incidentId, string cardId, string message)
        {
            await _cardService.Reject(incidentId, cardId, message);
        }

        public async Task<CardStatsticDto[]> GetStatistics(
            StatsticType statisticType,
            StatsticPeriodType periodType,
            int periodCount)
        {
            var serviceTypeId = await _serviceTypeIdService.GetCurrentServiceTypeId();
            var statistics = await _cardService.GetStatictics(
                new CardGetStaticticsRequest()
                {
                    ServiceTypeIds = new[] { serviceTypeId },
                    PeriodType = (int)periodType,
                    PeriodCount = periodCount,
                    Type = (int)statisticType
                });
            return statistics.Select(x => _mapper.Map<CardStatstic, CardStatsticDto>(x)).ToArray();
        }

        public async Task<string> SubscribeToEmergencyAlerts()
        {
            var filter = new CardFilter()
            {
                ServiceTypeIds = new[] { _serviceTypeIdConfiguration.SaveCityServiceTypeId },
                CardIndexCodes = EmergencyAlertConfiguration.GetEmergencyAlertCardIndexCodes(_configuration)
            };
            return await _cardService.SubscribeToEmergencyAlerts(filter);
        }

        public async Task RefreshEmergencyAlerts(string sessionId, IHubClients clients)
        {
            var alerts = await _cardService.GetEmergencyAlerts(sessionId);
            await clients.All.SendAsync(UpdateEmergencyCardMethod, _mapper.Map<CardsListDto>(alerts));
        }

        #endregion

        private async Task<CardsListDto> GetCardsByFilter(CardFilterDto cardFilterDto, CardJournalType cardJournalType, string connectionId)
        {
            var filter = _mapper.Map<CardFilterDto, CardFilter>(cardFilterDto);
            var changes = await _cardService.GetCardsAsync(filter, cardJournalType, connectionId);
            return _mapper.Map<CardsListDto>(changes);
        }
    }
}
