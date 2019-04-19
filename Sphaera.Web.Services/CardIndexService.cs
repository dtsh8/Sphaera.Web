using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Models.Cards;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface ICardIndexService
    {
        Task<CardIndex[]> GetTree(long serviceType, int dispatchServiceId);

        Task<CardIndex[]> GetLevel(long serviceType, string parent, int dispatchServiceId);

        Task<CardIndex> Get(long serviceType, string code, int dispatchServiceId);

        Task<CardIndex[]> Apply(long serviceType, string parent, CardIndex[] data);
    }

    [UsedImplicitly]
    public class CardIndexService : SeviceBaseSimple, ICardIndexService
    {
        #region Private Consts

        #region Private Consts

        private const string GetCardIndexTreeUri = "/api/v1/CardIndex/GetTree?serviceTypeId={0}&dispatchServiceId={1}";
        private const string GetCardIndexLevelUri = "/api/v1/CardIndex/GetLevel?serviceTypeId={0}&parentCode={1}&dispatchServiceId={2}";
        private const string GetCardIndexObjectUri = "/api/v1/CardIndex/Get?serviceTypeId={0}&code={1}&dispatchServiceId={2}";
        private const string SetCardIndexLevelUri = "/api/v1/CardIndex/ApplyLevel?serviceTypeId={0}&parentCode={1}";

        #endregion

        #endregion

        #region Constructor

        public CardIndexService([NotNull] IConfiguration config)
        {
            SvcUrl = config["EmergencyCardIndex_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<CardIndex[]> GetTree(long serviceType, int dispatchServiceId)
        {
            return await base.GetList<CardIndex>(string.Format(GetCardIndexTreeUri, serviceType, dispatchServiceId));
        }

        public async Task<CardIndex[]> GetLevel(long serviceType, string parent, int dispatchServiceId)
        {
            return await base.GetList<CardIndex>(string.Format(GetCardIndexLevelUri, serviceType, parent, dispatchServiceId));
        }

        public async Task<CardIndex> Get(long serviceType, string code, int dispatchServiceId)
        {
            return await base.Get<CardIndex>(string.Format(GetCardIndexObjectUri, serviceType, code, dispatchServiceId));
        }

        public async Task<CardIndex[]> Apply(long serviceType, string parent, CardIndex[] data)
        {
            return await base.BulkUpdate(string.Format(SetCardIndexLevelUri, serviceType, parent), data);
        }

        #endregion
    }
}