using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// ЕДДС
    /// </summary>
    [DataContract]
    public class DispatchService
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}