using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sphaera.Web.Api.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        public IActionResult Get()
        {
            var result = new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            return result;
        }
    }
}