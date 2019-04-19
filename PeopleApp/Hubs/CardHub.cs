using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PeopleApp.Proxies;
using Sphaera.Web.Api.Extensions;
using Sphaera.Web.Api.Interfaces;
using Sphaera.Web.Core.Enum;
using Sphaera.Web.PeoplePublicArea.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace PeopleApp.Hubs
{
    public class CardHub : Hub<CardDto>, ISignalRHub
    {
        #region Public Consts

        public const string Path = @"/hubs/cards";

        #endregion

        #region Private Fields

        
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ICards _cards;

        private readonly Lazy<ILogger> _logger;

        #endregion

        #region Cinstructor

        public CardHub( IHttpContextAccessor contextAccessor,
                        ILoggerFactory loggerFactory,
                        ICards cards)
        {
            _contextAccessor = contextAccessor;
            _cards = cards;

            _logger = new Lazy<ILogger>(loggerFactory.CreateLogger<CardHub>);
        }

        #endregion

        #region Public Methods

        public void Initialize(HubRouteBuilder builder)
        {
            builder.MapHub<CardHub>(Path);
        }

        public async Task<CardsListDto> GetCards(CardFilterDto filter)
        {
            try
            {
                InitUser();
                return await _cards.GetCards(filter, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return new CardsListDto()
                {
                    Cards = new CardDto[0],
                    SessionId = null
                };
            }
        }

        public async Task<CardsListDto> GetAppealCards(CardFilterDto filter)
        {
            try
            {
                InitUser();
                string userName = Context.User?.Identity?.Name;
                return await _cards.GetAppealCards(filter, userName, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return new CardsListDto
                {
                    Cards = new CardDto[0],
                    SessionId = null
                };
            }
        }

        public async Task<CardDto> InsertCard(CardDto dto, string orgCode)
        {
            try
            {
                InitUser();
                return await _cards.Insert(dto, orgCode);
            }
            catch (Exception ex)
            {
                var resultException = ex.Parse();
                _logger.Value.LogError(ex, resultException.Message);
                return null;
            }
        }

        public async Task<CardDto> UpdateCard(CardDto dto, string orgCode)
        {
            try
            {
                InitUser();
                return await _cards.Update(dto, orgCode);
            }
            catch (Exception ex)
            {
                var resultException = ex.Parse();
                _logger.Value.LogError(ex, resultException.Message);
                return null;
            }
        }

        public async Task CloseCard(string incidentId, string cardId)
        {
            try
            {
                InitUser();
                await _cards.Close(incidentId, cardId);
            }
            catch (Exception ex)
            {
                var resultException = ex.Parse();
                _logger.Value.LogError(ex, resultException.Message);
            }
        }

        public async Task RejectCard(string incidentId, string cardId, string message)
        {
            try
            {
                InitUser();
                await _cards.Reject(incidentId, cardId, message);
            }
            catch (Exception ex)
            {
                var resultException = ex.Parse();
                _logger.Value.LogError(ex, resultException.Message);
            }
        }

        public async Task<CardStatsticDto[]> GetStatistics()
        {
            try
            {
                return await _cards.GetStatistics(StatsticType.IncidentsOpenCount | StatsticType.IncidentsFinishedCount,
                    StatsticPeriodType.LastHours, 24);
            }
            catch (Exception ex)
            {
                var resultException = ex.Parse();
                _logger.Value.LogError(ex, resultException.Message);
                return new CardStatsticDto[0];
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _cards.Unsubscribe(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        #endregion

        private void InitUser()
        {
            _contextAccessor.HttpContext = Context.GetHttpContext();
            var userName = _contextAccessor.HttpContext.User.Identity.Name;
            _logger.Value.LogInformation($"Context User: {userName}");
        }

        private void LogException(Exception ex)
        {
            var resultException = ex.Parse();
            _logger.Value.LogError(ex, resultException.Message);
        }
    }
}
