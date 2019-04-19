using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Тип службы реагирования
    /// </summary>
    [DataContract]
    public class ServiceType
    {
        [JsonProperty(PropertyName = "id")]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
    }
}