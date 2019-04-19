using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    public class IncidentGetRequest
    {
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }
        
        [DataMember(Name = "cardId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }
        
        [DataMember(Name = "exceptServiceTypeIds", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "exceptServiceTypeIds")]
        public long[] ExceptServiceTypeIds { get; set; }
    }
}