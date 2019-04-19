using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface IMapObjectsService
    {
        [NotNull]
        Task<VideoCamera[]> GetList(string mapExtent);
    }

    [UsedImplicitly]
    public class MapObjectsService : SeviceBaseSimple, IMapObjectsService
    {
        private const string AddressSearchUri = "/api/v1/VideoCamera/Get?mapBounds={0}";

        public MapObjectsService([NotNull] IConfiguration config)
        {
            SvcUrl = config["MapObjects_URI"];
        }

        public async Task<VideoCamera[]> GetList(string mapBounds)
        {
            return await base.GetList<VideoCamera>(string.Format(AddressSearchUri, mapBounds));
        }
    }
}