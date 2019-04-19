using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.AddressController
{
    public class Street
    {
        [DataMember(Name = "streetFiasCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "streetFiasCode")]
        public string StreetFiasCode { get; set; }
        
        [DataMember(Name = "municipalityFiasCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "municipalityFiasCode")]
        public string MunicipalityFiasCode { get; set; }
        
        [DataMember(Name = "localityFiasCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localityFiasCode")]
        public string LocalityFiasCode { get; set; }
        
        [DataMember(Name = "streetSuggestion", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "streetSuggestion")]
        public string StreetSuggestion { get; set; }
        
        [DataMember(Name = "streetFullName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "streetFullName")]
        public string StreetFullName { get; set; }
        
        [DataMember(Name = "streetTypeName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "streetTypeName")]
        public string StreetTypeName { get; set; }
        
        [DataMember(Name = "streetName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "streetName")]
        public string StreetName { get; set; }
    }
}