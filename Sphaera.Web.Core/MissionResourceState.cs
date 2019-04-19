using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Статус ресурса реагирования
    /// </summary>
    [DataContract]
    public class MissionResourceState : ResourceState
    {
        /// <summary>
        /// Признак статуса назначения
        /// </summary>
        [DataMember(Name = "assigned", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "assigned")]
        public bool Assigned { get; set; }
    }
}