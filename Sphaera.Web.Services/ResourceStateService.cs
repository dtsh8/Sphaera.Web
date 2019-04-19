using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IResourceStateService
    {
        Task<GlobalResourceState[]> GetGlobalList(long serviceType, string resourceType, bool enabledOnly);

        Task<MissionResourceState[]> GetMissionList(long serviceType, string resourceType, bool enabledOnly);

        Task SetGlobal(GlobalResourceState data);

        Task SetMission(MissionResourceState data);
    }

    [UsedImplicitly]
    public class ResourceStateService : SeviceBaseSimple, IResourceStateService
    {
        #region Private Consts

        private const string GetGlobalResourceStateListUri = "/api/v1/ResourceState/GetGlobal";
        private const string GetMissionResourceStateListUri = "/api/v1/ResourceState/GetMission";
        private const string PutGlobalResourceStateUri = "/api/v1/ResourceState/PutGlobalState";
        private const string PutMissionResourceStateUri = "/api/v1/ResourceState/PutMissionState";

        #endregion

        #region Constructor

        public ResourceStateService([NotNull] IConfiguration config)
        {
            SvcUrl = config["ResourceState_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<GlobalResourceState[]> GetGlobalList(long serviceType, string resourceType, bool enabledOnly)
        {
            var getParameters = HttpUtility.ParseQueryString("");
            getParameters["serviceTypeId"] = serviceType.ToString();
            if (!string.IsNullOrEmpty(resourceType))
            {
                getParameters["resourceTypeCode"] = resourceType;
            }
            getParameters["enabledOnly"] = enabledOnly.ToString().ToLower();
            var url = $"{GetGlobalResourceStateListUri}?{getParameters}";
            return await base.GetList<GlobalResourceState>(url);
        }

        public async Task<MissionResourceState[]> GetMissionList(long serviceType, string resourceType, bool enabledOnly)
        {
            var getParameters = HttpUtility.ParseQueryString("");
            getParameters["serviceTypeId"] = serviceType.ToString();
            if (!string.IsNullOrEmpty(resourceType))
            {
                getParameters["resourceTypeCode"] = resourceType;
            }
            getParameters["enabledOnly"] = enabledOnly.ToString().ToLower();
            var url = $"{GetMissionResourceStateListUri}?{getParameters}";
            return await base.GetList<MissionResourceState>(url);
        }

        public async Task SetGlobal(GlobalResourceState data)
        {
            await base.Update(PutGlobalResourceStateUri, data);
        }

        public async Task SetMission(MissionResourceState data)
        {
            await base.Update(PutMissionResourceStateUri, data);
        }

        #endregion
    }
}