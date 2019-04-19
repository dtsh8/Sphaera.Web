using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Extensions;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IResourceService
    {
        [NotNull]
        Task<AssignedResource[]> GetAssigned(string missionId);

        [NotNull]
        Task Refresh([NotNull] Action<string, AssignedResource[]> sender);

        [NotNull]
        Task<Resource[]> GetAvailable(long serviceTypeId, [NotNull] string stationCode);

        [NotNull]
        Task<ResourceEvent[]> GetHistory(string missionId, [NotNull] string resourceCode);

        [NotNull]
        Task<ResourceState> GetState(long serviceTypeId, [NotNull] string resourceCode);

        [NotNull]
        Task<string> GetMission(long serviceTypeId, [NotNull] string resourceCode);

        [NotNull]
        Task<bool> SetMissionStateChange([NotNull] ResourceMissionStateChange obj);

        [NotNull]
        Task<bool> SetGlobalStateChange([NotNull] ResourceGlobalStateChange obj);
    }

    public class ResourceService : SeviceBaseSimple, IResourceService
    {
        #region Private Consts

        private const string GetAvailableResources = "/api/v1/Resource/GetAvailableResources?serviceTypeId={0}&stationCode={1}";
        private const string GetAssignedResources = "/api/v1/Resource/GetAssignedResources";
        private const string GetResourceHistory = "/api/v1/Resource/GetResourceHistory?missionId={0}&resourceCode={1}";
        private const string GetResourceState = "/api/v1/Resource/GetResourceState?serviceTypeId={0}&resourceCode={1}";
        private const string GetResourceMission = "/api/v1/Resource/GetResourceMission?serviceTypeId={0}&resourceCode={1}";
        private const string SetMissionState = "/api/v1/Resource/SetMissionState";
        private const string SetResourceState = "/api/v1/Resource/SetResourceState";

        #endregion

        #region Private Fields

        [NotNull]
        private readonly ConcurrentDictionary<string, string> _missions = new ConcurrentDictionary<string, string>();

        [NotNull]
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly WebApiProxy _webApiProxy;

        #endregion

        #region Constructor

        public ResourceService([NotNull] IHttpContextAccessor contextAccessor,
                               [NotNull] IConfiguration config)
        {
            _contextAccessor = contextAccessor;
            SvcUrl = config["ResourceSvc_URI"];
            _webApiProxy = new WebApiProxy(SvcUrl);
        }

        #endregion

        #region Public Methods

        public async Task<AssignedResource[]> GetAssigned(string missionId)
        {
            _missions.AddOrUpdate(_contextAccessor.GetUserId(), missionId, (key, oldValue) => missionId);
            var request = new AssignedResourceRequest();
            request.MissionIds = new[] { missionId };
            return await _webApiProxy.PostAsync<AssignedResourceRequest, AssignedResource[]>(GetAssignedResources, request);
        }

        public async Task Refresh(Action<string, AssignedResource[]> sender)
        {
            if (_missions.IsEmpty)
                return;

            foreach (var mission in _missions)
            {
                var request = new AssignedResourceRequest();
                request.MissionIds = new[] { mission.Value };
                var changes = await _webApiProxy.PostAsync<AssignedResourceRequest, AssignedResource[]>(GetAssignedResources, request);
                if (changes.Any())
                    sender(mission.Key, changes);
            }
        }

        public async Task<Resource[]> GetAvailable(long serviceTypeId, string stationCode)
        {
            return await base.GetList<Resource>(string.Format(GetAvailableResources, serviceTypeId, stationCode));
        }

        public async Task<ResourceEvent[]> GetHistory(string missionId, string resourceCode)
        {
            return await base.GetList<ResourceEvent>(string.Format(GetResourceHistory, missionId, resourceCode));
        }

        public async Task<ResourceState> GetState(long serviceTypeId, string resourceCode)
        {
            return await base.Get<ResourceState>(string.Format(GetResourceState, serviceTypeId, resourceCode));
        }

        public async Task<string> GetMission(long serviceTypeId, string resourceCode)
        {
            return await base.Get<string>(string.Format(GetResourceMission, serviceTypeId, resourceCode));
        }

        public async Task<bool> SetMissionStateChange(ResourceMissionStateChange obj)
        {
            return await base.Set<ResourceMissionStateChange, bool>(SetMissionState, obj);
        }

        public async Task<bool> SetGlobalStateChange(ResourceGlobalStateChange obj)
        {
            return await base.Set<ResourceGlobalStateChange, bool>(SetResourceState, obj);
        }

        #endregion
    }
}