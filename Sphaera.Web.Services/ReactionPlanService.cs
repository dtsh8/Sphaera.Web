using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IReactionPlanService
    {
        Task<ReactionPlan[]> GetList(string incidentId, string cardId);
    }

    [UsedImplicitly]
    public class ReactionPlanService : SeviceBaseSimple, IReactionPlanService
    {
        #region Private Consts

        private const string ReactionPlanUri = "/api/v1/ReactionPlan/Get?incidentId={0}&cardId={1}";

        #endregion

        #region Constructor

        public ReactionPlanService([NotNull] IConfiguration config)
        {
            SvcUrl = config["ReactionPlan_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<ReactionPlan[]> GetList(string incidentId, string cardId)
        {
            return await base.GetList<ReactionPlan>(string.Format(ReactionPlanUri, incidentId, cardId));
        }

        #endregion
    }
}