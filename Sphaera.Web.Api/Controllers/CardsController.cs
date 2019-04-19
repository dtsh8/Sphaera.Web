using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphaera.Web.Core.Cards;

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
                new Card { CardId = "1", IncidentId="222", Comment  = "lorem 2222 ipsum" },
                new Card { CardId = "2", IncidentId="555", Comment = "lorem 5555 ipsum" },
                new Card { CardId = "3", IncidentId="333", Comment = "lorem 3333 ipsum" },
                new Card { CardId = "4", IncidentId="123", Comment = "lorem 123 ipsum" },
            };
            return new JsonResult(cards);
        }
    }

    //public class Card
    //{
    //    public int CardId { get; set; }
    //    public int IncidentId { get; set; }

    //    public bool IsArchived { get; set; }

    //    public string Description { get; set; }
    //}
}