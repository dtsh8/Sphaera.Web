using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Статус ресурса реагирования
    /// </summary>
    [DataContract]
    public class ResourceState
    {
        /// <summary>
        /// Код статуса ресурса реагирования
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Название статуса ресурса реагирования
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание статуса ресурса реагирования
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Признак активности статуса
        /// </summary>
        [DataMember(Name = "enabled", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Идентификатор типа службы реагирования
        /// </summary>
        [DataMember(Name = "serviceTypeId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeId")]
        public long ServiceTypeId { get; set; }

        /// <summary>
        /// Код типа ресурса реагирования
        /// </summary>
        [DataMember(Name = "resourceTypeCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "resourceTypeCode")]
        public string ResourceTypeCode { get; set; }
    }
}