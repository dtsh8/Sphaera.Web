using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sphaera.Web.Core.Archive;
using Sphaera.Web.Core.Cards;
using Sphaera.Web.Core.Enum;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sphaera.Web.Api.Services
{
    public interface ICardService
    {
        Task<CardsList> GetCardsAsync(CardFilter filter, string connectionId = null);

        /// <summary>
        /// Возвращает карточки для заданного типа журнала.
        /// </summary>
        Task<CardsList> GetCardsAsync(CardFilter filter, CardJournalType cardJournalType, string connectionId = null);

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

        /// <summary>
        /// По коннекшну и типу журнала храним sessionId, вернутый для фильтра.
        /// </summary>
        private readonly ConcurrentDictionary<(string connectionId, CardJournalType cardJournalType), string> _usersSessions =
            new ConcurrentDictionary<(string connectionId, CardJournalType cardJournalType), string>();

        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory _logger;
        private static IConfiguration _config;
        //private object _remoteServiceBaseUrl;
        private readonly string _baseSvcAddress ;
        public CardService(HttpClient httpClient, IConfiguration config, ILoggerFactory logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _config = config;
            _baseSvcAddress = _config["Crd_Uri"];
        }

        public Task<bool> Close(string incidentId, string cardId)
        {
            throw new NotImplementedException();
        }

        public Task<ArchiveCards> GetArchive(ArchiveCardFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<CardsList> GetCardsAsync(CardFilter filter, string connectionId = null)
        {
            return await GetCardsAsync(filter, CardJournalType.Accident, connectionId);
        }

        public async Task<CardsList> GetCardsAsync(CardFilter filter, CardJournalType cardJournalType, string connectionId = null)
        {
            var uri = API.Cards.GetCardsUri(_baseSvcAddress, GetCardUri);
            var subsUri = API.Cards.GetSubscribeUri(_baseSvcAddress, GetCardUri);
            var httpMsg = await _httpClient.PostAsJsonAsync(SubscribeCardUri, filter);
            var sId = await GetResultFromResponseMessage<string>(httpMsg);

            var responseString = await _httpClient.GetStringAsync(uri);
            var cards = JsonConvert.DeserializeObject<Card[]>(responseString);

            return new CardsList { Cards = cards, SessionId = sId };
        }

        public Task<ICollection<Card>> GetCards(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCardsSessionId(CardFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<CardsList> GetEmergencyAlerts(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<CardStatstic[]> GetStatictics(params CardGetStaticticsRequest[] request)
        {
            throw new NotImplementedException();
        }

        public Task Refresh(Action<string, CardJournalType, CardsList> sender)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Reject(string incidentId, string cardId, string message)
        {
            throw new NotImplementedException();
        }

        public Task<string> SubscribeToEmergencyAlerts(CardFilter filter)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string connectionId)
        {
            throw new NotImplementedException();
        }

        public Task<Card> Update(Card card, string orgCode)
        {
            throw new NotImplementedException();
        }


        private static async Task<TResult> GetResultFromResponseMessage<TResult>(HttpResponseMessage httpResponseMessage)
        {
            //if (!httpResponseMessage.IsSuccessStatusCode)
            //{
            //    throw GetApiException(httpResponseMessage);
            //}

            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            //if (string.IsNullOrEmpty(jsonString))
            //{
            //    throw new ApiModelException(Errors.EmptyResult);
            //}

            return JsonConvert.DeserializeObject<TResult>(jsonString);
        }

    }
}
