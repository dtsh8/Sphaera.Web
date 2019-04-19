using Microsoft.AspNetCore.SignalR;

namespace Sphaera.Web.Api.Interfaces
{
    public interface ISignalRHub
    {
        void Initialize(HubRouteBuilder builder);
    }
}
