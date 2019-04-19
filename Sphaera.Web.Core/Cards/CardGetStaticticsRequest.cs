using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Cards
{
    public class CardGetStaticticsRequest
    {
        [DataMember(Name = "serviceTypeIds", EmitDefaultValue = true)]
        [JsonProperty(PropertyName = "serviceTypeIds")]
        public long[] ServiceTypeIds { get; set; }
        
        [DataMember(Name = "type", EmitDefaultValue = true)]
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }
        
        [DataMember(Name = "periodType", EmitDefaultValue = true)]
        [JsonProperty(PropertyName = "periodType")]
        public int PeriodType { get; set; }
        
        [DataMember(Name = "periodCount", EmitDefaultValue = true)]
        [JsonProperty(PropertyName = "periodCount")]
        public int PeriodCount { get; set; }
    }
}