using Microsoft.AspNetCore.Builder;

namespace Sphaera.Web.Api.Interfaces
{
    public interface ISoapAction
    {
        void Initialize(IApplicationBuilder builder);
    }
}
