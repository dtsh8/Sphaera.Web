using Sphaera.Web.Core.Enum;

namespace Sphaera.Web.PeoplePublicArea.Models.Dto
{
    public class CardStatsticDto
    {
        public int? Type { get; set; }

        public double? Value { get; set; }

        public StatsticPeriodType? PeriodType { get; set; }
        
        public int? PeriodCount { get; set; }
    }
}