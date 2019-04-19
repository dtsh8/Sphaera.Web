using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Core;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;
using Sphaera.Web.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IIncidentStateService
    {
        Task<IncidentState[]> GetList();

        /// <summary>
        /// Возвращает статус происшествия "Зарегистрировано".
        /// </summary>
        Task<IncidentState> GetNew();

        /// <summary>
        /// Возвращает статус происшествия "В работе".
        /// </summary>
        Task<IncidentState> GetAccepted();

        /// <summary>
        /// Возвращает статус происшествия "Реагирование завершено".
        /// </summary>
        Task<IncidentState> GetClosed();
    }

    //[UsedImplicitly]
    public class IncidentStateService : SeviceBase<IncidentState>, IIncidentStateService
    {
        #region Private Consts

        private const string IncidentStateUri = "/api/v1/IncidentState/Get";

        #endregion

        #region Constructor

        public IncidentStateService([NotNull] IConfiguration config) : base(TimeSpan.FromMinutes(60d))
        {
            SvcUrl = config["IncidentState_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<IncidentState[]> GetList()
        {
            return await base.GetList(IncidentStateUri, (cache, obj) => { cache.Add(obj.Id, obj); });
        }

        public async Task<IncidentState> GetNew() => 
            await GetByCode("NEW");

        public async Task<IncidentState> GetAccepted() => 
            await GetByCode("ACCEPTED");

        public async Task<IncidentState> GetClosed() => 
            await GetByCode("CLOSED");

        #endregion

        private async Task<IncidentState> GetByCode(string code)
        {
            var statuses = await GetList();
            return statuses.First(t => t.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}