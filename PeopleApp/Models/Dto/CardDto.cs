using System;

namespace Sphaera.Web.PeoplePublicArea.Models.Dto
{
    public class CardDto
    {
        public string IncidentId { get; set; }

        public string CardId { get; set; }

        public long StateId { get; set; }

        public string StateName { get; set; }

        public string CardIndexCode { get; set; }

        public string CardIndexName { get; set; }

        public long ServiceTypeId { get; set; }

        public string ServiceTypeName { get; set; }

        public string Comment { get; set; }

        public DateTime? Created { get; set; }

        public bool? IsDanger { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool? IsOverdue { get; set; }

        public string OverdueOperation { get; set; }

        public string ShortAddress { get; set; }

        public string StationCode { get; set; }

        public DateTime? ChatReviewed { get; set; }

        public bool NewChatMessages { get; set; }

        public int DispatchServiceId { get; set; }

        public string DispatchServiceName { get; set; }

        public string MissionId { get; set; }
    }
}