using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Статус ресурса реагирования
    /// </summary>
    [DataContract]
    public class GlobalResourceState : ResourceState
    {
        /// <summary>
        /// Признак доступности для назначения
        /// </summary>
        [DataMember(Name = "available", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "available")]
        public bool Available { get; set; }
    }
}