using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sphaera.Web.Core.Archive;
using Sphaera.Web.Core.Cards;
using Sphaera.Web.Core.Enum;
using Sphaera.Web.Server.Extensions;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Models.Archive;
using Sphaera.Web.Server.Models.Cards;
using Sphaera.Web.Server.Models.Enum;
using XData.Extensions;

namespace Sphaera.Web.Services
{
    public interface ICardService
    {
        Task<CardsList> GetCards(CardFilter filter, string connectionId = null);

        /// <summary>
        /// Возвращает карточки для заданного типа журнала.
        /// </summary>
        Task<CardsList> GetCards(CardFilter filter, CardJournalType cardJournalType, string connectionId = null);

        Task<string> GetCardsSessionId(CardFilter filter);

        Task<ICollection<Card>> GetCards(string sessionId);

        Task Refresh(Action<string, CardJournalType, CardsList> sender);

        void Unsubscribe(string connectionId);

        /// <summary>
        /// Для вставки и редактирования только для лк ЭОС.
        /// </summary>
        /// <param name="card">Изменения карточки реагирования</param>
        /// <param name="orgCode">Код внешней организации</param>
        /// <returns>Измененная карточка реагирования</returns>
        Task<Card> Update(Card card, string orgCode);

        Task<bool> Close(string incidentId, string cardId);

        Task<bool> Reject(string incidentId, string cardId, string message);

        Task<CardStatstic[]> GetStatictics(params CardGetStaticticsRequest[] request);

        /// <summary>
        /// Подписаться на чрезвычайные ситуации.
        /// </summary>
        /// <returns>Идентификатор сессии подписки.</returns>
        Task<string> SubscribeToEmergencyAlerts(CardFilter filter);

        /// <summary>
        /// Получить чрезвычайные ситуации.
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии подписки.</param>
        /// <returns>Список карточек реагирования.</returns>
        Task<CardsList> GetEmergencyAlerts(string sessionId);

        /// <summary>
        /// Возвращает архивные карточки реагирования.
        /// </summary>
        /// <param name="filter">Фильтр архивных карточек</param>
        Task<ArchiveCards> GetArchive(ArchiveCardFilter filter);
    }

    public class CardService : ICardService
    {
        #region Private Consts

        private const string SubscribeCardUri = "/api/v1/Card/Subscribe";

        private const string GetCardUri = "/api/v1/Card/Get?sessionId={0}";

        private const string PutCardUri = "/api/v1/Card/Put?orgCode={0}";

        private const string CloseCardUri = "/api/v1/Card/Close?incidentId={0}&cardId={1}";

        private const string RejectCardUri = "/api/v1/Card/Reject?incidentId={0}&cardId={1}&message={2}";

        private const string GetStaticticsUri = "/api/v1/Card/GetStatictics";

        private const string GetArchiveCardsUri = "/api/v1/Card/GetArchive";

        #endregion

        #region Private Fields

        /// <summary>
        /// По коннекшну и типу журнала храним sessionId, вернутый для фильтра.
        /// </summary>
        private readonly ConcurrentDictionary<(string connectionId, CardJournalType cardJournalType), string> _usersSessions =
            new ConcurrentDictionary<(string connectionId, CardJournalType cardJournalType), string>();

        //[NotNull]
        //private readonly IHttpContextAccessor _contextAccessor;

        //[NotNull]
        private readonly string _cardsSvc;

        private readonly Lazy<ILogger> _logger;

        //public object CardFilterHelper { get; private set; }

        #endregion

        #region Constructor

        public CardService(
           
           IConfiguration config,
             loggerFactory)
        {
            //_contextAccessor = contextAccessor;

            _cardsSvc = config["EmergencyCard_URI"];
            _logger = new Lazy<ILogger>(loggerFactory.CreateLogger<CardService>);
        }

        #endregion

        #region Public Methods

        public async Task<CardsList> GetCards(CardFilter filter, string connectionId = null)
        {
            return await GetCards(filter, CardJournalType.Accident, connectionId);
        }

        public async Task<CardsList> GetCards(CardFilter filter, CardJournalType cardJournalType, string connectionId = null)
        {
            filter = CardFilterHelper.ClearCardFilter(filter);
            var cardControl = new WebApiProxy(_cardsSvc);

            _logger.Value.LogInformation(
                $"GetCards JournalType: {cardJournalType} CardFilter: {JsonConvert.SerializeObject(filter)}");
            var sessionId = await cardControl.PostAsync<CardFilter, string>(SubscribeCardUri, filter);

            if (connectionId != null)
            {
                _usersSessions.AddOrUpdate((connectionId: connectionId, cardJournalType: cardJournalType), _ => sessionId,
                    (_, __) => sessionId);
            }

            var cards = await cardControl.GetResultAsync<Card[]>(string.Format(GetCardUri, sessionId));
            return new CardsList()
            {
                Cards = cards,
                SessionId = sessionId
            };
        }

        public async Task<string> GetCardsSessionId(CardFilter filter)
        {
            filter = CardFilterHelper.ClearCardFilter(filter);
            var cardControl = new WebApiProxy(_cardsSvc);
            string sessionId = await cardControl.PostAsync<CardFilter, string>(SubscribeCardUri, filter);
            return sessionId;
        }

        public async Task<ICollection<Card>> GetCards(string sessionId)
        {
            var cardControl = new WebApiProxy(_cardsSvc);
            var cards = await cardControl.GetResultAsync<ICollection<Card>>(string.Format(GetCardUri, sessionId));
            return cards;
        }

        public async Task Refresh(Action<string, CardJournalType, CardsList> sender)
        {
            if (_usersSessions.IsEmpty)
                return;

            var cardControl = new WebApiProxy(_cardsSvc);
            foreach (var keyValue in _usersSessions)
            {
                var (connectionId, cardJournalType) = keyValue.Key;
                var sessionId = keyValue.Value;
                var cards = await cardControl.GetResultAsync<Card[]>(string.Format(GetCardUri, sessionId));
                if (cards.Length > 0)
                {
                    var cardsList = new CardsList()
                    {
                        SessionId = sessionId,
                        Cards = cards
                    };
                    sender(connectionId, cardJournalType, cardsList);
                }
            }
        }

        public void Unsubscribe(string connectionId)
        {
            _usersSessions.RemoveAll(key => string.Equals(connectionId, key.connectionId));
        }

        /// <inheritdoc/>
        public async Task<Card> Update(Card card, string orgCode)
        {
            string apiPath = string.Format(PutCardUri, orgCode);
            var cardControl = new WebApiProxy(_cardsSvc);
            return await cardControl.PutAsync(apiPath, card);
        }

        public async Task<bool> Close(string incidentId, string cardId)
        {
            var cardControl = new WebApiProxy(_cardsSvc);
            return await cardControl.PostRequestAsync<bool>(string.Format(CloseCardUri, incidentId, cardId));
        }

        public async Task<bool> Reject(string incidentId, string cardId, string message)
        {
            var cardControl = new WebApiProxy(_cardsSvc);
            return await cardControl.PostRequestAsync<bool>(string.Format(RejectCardUri, incidentId, cardId, message));
        }

        public async Task<CardStatstic[]> GetStatictics(params CardGetStaticticsRequest[] request)
        {
            var cardControl = new WebApiProxy(_cardsSvc);
            return await cardControl.PostAsync<CardGetStaticticsRequest[], CardStatstic[]>(GetStaticticsUri, request);
        }

        public async Task<string> SubscribeToEmergencyAlerts(CardFilter filter)
        {
            filter = CardFilterHelper.ClearCardFilter(filter);
            var cardControl = new WebApiProxy(_cardsSvc);
            return await cardControl.PostAsync<CardFilter, string>(SubscribeCardUri, filter);
        }

        public async Task<CardsList> GetEmergencyAlerts(string sessionId)
        {
            var cardControl = new WebApiProxy(_cardsSvc);
            var cards = await cardControl.GetResultAsync<Card[]>(string.Format(GetCardUri, sessionId));
            return new CardsList()
            {
                SessionId = sessionId,
                Cards = cards
            };
        }

        public async Task<ArchiveCards> GetArchive(ArchiveCardFilter filter)
        {
            var cardControl = new WebApiProxy(_cardsSvc);
            return await cardControl.PostAsync<ArchiveCardFilter, ArchiveCards>(GetArchiveCardsUri, filter);
        }

        #endregion
    }
}
