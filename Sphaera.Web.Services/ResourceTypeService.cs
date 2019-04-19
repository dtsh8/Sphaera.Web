using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IResourceTypeService
    {
        Task<ResourceType[]> GetList(long serviceType);

        Task Update(ResourceType data);

        Task Delete(long serviceType, string resTypeCode);
    }

    [UsedImplicitly]
    public class ResourceTypeService : SeviceBase<ResourceType>, IResourceTypeService
    {
        #region Private Consts

        private const string GetResourceTypeListUri = "/api/v1/ResourceType/Get?serviceTypeId={0}";
        private const string PutResourceTypeUri = "/api/v1/ResourceType/Put";
        private const string DelResourceTypeUri = "/api/v1/ResourceType/Delete?serviceTypeId={0}&code={1}";

        #endregion

        #region Constructor

        public ResourceTypeService([NotNull] IConfiguration config) : base(config)
        {
            SvcUrl = config["ResourceType_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<ResourceType[]> GetList(long serviceType)
        {
            return await base.GetList(string.Format(GetResourceTypeListUri, serviceType), (cache, obj) => { cache.Add(obj.Code, obj); });
        }

        public async Task Update(ResourceType data)
        {
            await base.Update(PutResourceTypeUri, data.Code, data);
        }

        public async Task Delete(long serviceType, string resTypeCode)
        {
            await base.Delete(string.Format(DelResourceTypeUri, serviceType, resTypeCode), resTypeCode);
        }

        #endregion
    }
}