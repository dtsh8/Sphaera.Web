using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.AddressController
{
    public class Locality
    {
        [DataMember(Name = "localityFiasCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localityFiasCode")]
        public string LocalityFiasCode { get; set; }
        
        [DataMember(Name = "municipalityFiasCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "municipalityFiasCode")]
        public string MunicipalityFiasCode { get; set; }
        
        [DataMember(Name = "localitySuggestion", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localitySuggestion")]
        public string LocalitySuggestion { get; set; }

        [DataMember(Name = "localityFullName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localityFullName")]
        public string LocalityFullName { get; set; }
        
        [DataMember(Name = "localityTypeName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localityTypeName")]
        public string LocalityTypeName { get; set; }
        
        [DataMember(Name = "localityName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localityName")]
        public string LocalityName { get; set; }
    }
}