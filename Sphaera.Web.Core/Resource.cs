using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Ресурс
    /// </summary>
    [DataContract]
    public class Resource
    {
        /// <summary>
        /// Gets or Sets ResourceCode
        /// </summary>
        [DataMember(Name = "resourceCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "resourceCode")]
        public string ResourceCode { get; set; }

        /// <summary>
        /// Gets or Sets ServiceTypeId
        /// </summary>
        [DataMember(Name = "serviceTypeId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeId")]
        public long ServiceTypeId { get; set; }

        /// <summary>
        /// Gets or Sets StationCode
        /// </summary>
        [DataMember(Name = "stationCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stationCode")]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or Sets ResourceTypeCode
        /// </summary>
        [DataMember(Name = "resourceTypeCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "resourceTypeCode")]
        public string ResourceTypeCode { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}