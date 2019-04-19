using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IMunicipalityService
    {
        Task<Municipality[]> GetList();
    }

    [UsedImplicitly]
    public class MunicipalityService : SeviceBase<Municipality>, IMunicipalityService
    {
        #region Private Consts

        private const string MunicipalityUri = "/api/v1/Municipality/Get";

        #endregion

        #region Constructor

        public MunicipalityService([NotNull] IConfiguration config) : base(config)
        {
            SvcUrl = config["Municipality_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<Municipality[]> GetList()
        {
            return await base.GetList(MunicipalityUri, (cache, obj) => { cache.Add(obj.Id, obj); });
        }

        #endregion
    }
}