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
            //string[] strings = { "fdf", "erer" };
            var cards = new List<Card> {
                new Card { CardId = 1, IncidentId=222, IsArchived = false, Description = "lorem 2222 ipsum" },
                new Card { CardId = 2, IncidentId=555, IsArchived = false, Description = "lorem 5555 ipsum" },
                new Card { CardId = 3, IncidentId=333, IsArchived = false, Description = "lorem 3333 ipsum" },
                new Card { CardId = 4, IncidentId=123, IsArchived = true, Description = "lorem 123 ipsum" },
            };
            return new JsonResult(cards);
        }
    }

    public class Card
    {
        public int CardId { get; set; }
        public int IncidentId { get; set; }

        public bool IsArchived { get; set; }

        public string Description { get; set; }
    }
}