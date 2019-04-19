using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Шоссе
    /// </summary>
    [DataContract]
    public class Highway
    {
        /// <summary>
        /// Название/номер шоссе
        /// </summary>
        /// <value>Название/номер шоссе</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Километр шоссе
        /// </summary>
        /// <value>Километр шоссе</value>
        [DataMember(Name = "km", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "km")]
        public int? Km { get; set; }
    }
}