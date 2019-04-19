using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Municipality
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets FiasCode
        /// </summary>
        [DataMember(Name = "fiasCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "fiasCode")]
        public string FiasCode { get; set; }

        /// <summary>
        /// Gets or Sets Alias
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Gets or Sets IsCity
        /// </summary>
        [DataMember(Name = "isCity", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isCity")]
        public bool? IsCity { get; set; }
    }
}