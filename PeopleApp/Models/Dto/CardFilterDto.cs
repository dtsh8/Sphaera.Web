using System;
using System.Collections.Generic;

namespace Sphaera.Web.PeoplePublicArea.Models.Dto
{
    public class CardFilterDto
    { 
        public List<long> DispatchServiceIds { get; set; }

        public List<long> ServiceTypeIds { get; set; }

        public bool ExceptServiceTypeIds { get; set; }

        public string IncidentId { get; set; }

        public List<long> StateIds { get; set; }

        public bool ExceptStateIds { get; set; }

        public string[] CardIndexCodes { get; set; }

        public bool ExceptCardIndexCodes { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTill { get; set; }

        public bool? IsDanger { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Distance { get; set; }

        public string MapBounds { get; set; }

        public bool? IsOverdue { get; set; }

        public string OrganizationCode { get; set; }

        public string StationCode { get; set; }

        public string UserLogin { get; set; }
    }
}