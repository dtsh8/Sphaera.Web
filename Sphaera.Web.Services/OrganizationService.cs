using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IOrganizationService
    {
        /// <summary>
        /// Получение списка внешних организаций службы реагирования.
        /// </summary>
        [NotNull]
        Task<Organization[]> GetList(long serviceType);

        /// <summary>
        /// Получение списка всех внешних организаций.
        /// </summary>
        [NotNull]
        Task<Organization[]> GetList();

        [NotNull]
        Task<Organization> Get([NotNull] string code);

        [NotNull]
        Task Update([NotNull] Organization data);

        [NotNull]
        Task Delete([NotNull] string code);
    }

    [UsedImplicitly]
    public class OrganizationService : SeviceBase<Organization>, IOrganizationService
    {
        #region Private Consts

        private const string GetOrganizationListUri = "/api/v1/ExternalOrg/Get?serviceTypeId={0}";
        private const string PutOrganizationUri = "/api/v1/ExternalOrg/Put";
        private const string DelOrganizationUri = "/api/v1/ExternalOrg/Delete?orgCode={0}";

        #endregion

        #region Constructor

        public OrganizationService([NotNull] IConfiguration config) : base(config)
        {
            SvcUrl = config["ExternalOrg_URI"];
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public async Task<Organization[]> GetList(long serviceType)
        {
            return await base.GetList(string.Format(GetOrganizationListUri, serviceType), (cache, obj) => { cache.Add(obj.Code, obj); });
        }

        /// <inheritdoc/>
        public async Task<Organization[]> GetList()
        {
            return await GetList(0);
        }

        public async Task<Organization> Get(string code)
        {
            return await base.Get(code);
        }

        public async Task Update(Organization data)
        {
            await base.Update(PutOrganizationUri, data.Code, data);
        }

        public async Task Delete(string code)
        {
            await base.Delete(string.Format(DelOrganizationUri, code), code);
        }

        #endregion
    }
}