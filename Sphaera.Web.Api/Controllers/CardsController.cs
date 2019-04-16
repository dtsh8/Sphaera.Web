using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sphaera.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class CardsController : ControllerBase
    {
        public IActionResult Get()
        {
            string[] strings = { "fdf", "erer" };
            return new JsonResult(strings);
        }
    }
}