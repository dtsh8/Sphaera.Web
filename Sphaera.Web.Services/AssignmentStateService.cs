using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IAssignmentStateService
    {
        Task<AssignmentState[]> GetList();
    }

    [UsedImplicitly]
    public class AssignmentStateService : SeviceBase<AssignmentState>, IAssignmentStateService
    {
        #region Private Consts

        private const string AssignmentStateUri = "/api/v1/AssignmentState/Get";

        #endregion

        #region Constructor

        public AssignmentStateService([NotNull] IConfiguration config) : base(config)
        {
            SvcUrl = config["AssignmentState_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<AssignmentState[]> GetList()
        {
            return await base.GetList(AssignmentStateUri, (cache, obj) => { cache.Add(obj.Id, obj); });
        }

        #endregion
    }
}