using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Тип ресурса
    /// </summary>
    [DataContract]
    public class ResourceType
    {
        /// <summary>
        /// Код типа ресурса
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Идентификатор типа службы реагирования
        /// </summary>
        [DataMember(Name = "serviceTypeId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeId")]
        public long ServiceTypeId { get; set; }

        /// <summary>
        /// Описание типа ресурса
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}