using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IClaimStateService
    {
        Task<ClaimState[]> GetList();
    }

    [UsedImplicitly]
    public class ClaimStateService : SeviceBase<ClaimState>, IClaimStateService
    {
        #region Private Consts

        private const string ClaimStateUri = "/api/v1/ClaimState/Get";

        #endregion

        #region Constructor

        public ClaimStateService([NotNull] IConfiguration config) : base(config)
        {
            SvcUrl = config["ClaimState_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<ClaimState[]> GetList()
        {
            return await base.GetList(ClaimStateUri, (cache, obj) => { cache.Add(obj.Id, obj); });
        }

        #endregion
    }
}