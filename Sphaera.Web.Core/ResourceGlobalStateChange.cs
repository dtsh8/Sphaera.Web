using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Изменение глобального статуса ресурса реагирования
    /// </summary>
    [DataContract]
    public class ResourceGlobalStateChange
    {
        /// <summary>
        /// Идентификатор типа службы реагирования
        /// </summary>
        /// <value>Идентификатор типа службы реагирования</value>
        [DataMember(Name = "serviceTypeId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeId")]
        public long? ServiceTypeId { get; set; }

        /// <summary>
        /// Код ресурса реагирования
        /// </summary>
        /// <value>Код ресурса реагирования</value>
        [DataMember(Name = "resourceCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "resourceCode")]
        public string ResourceCode { get; set; }

        /// <summary>
        /// Момент смены статуса
        /// </summary>
        /// <value>Момент смены статуса</value>
        [DataMember(Name = "eventDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "eventDate")]
        public DateTime? EventDate { get; set; }

        /// <summary>
        /// Код статуса ресурса реагирования
        /// </summary>
        /// <value>Код статуса ресурса реагирования</value>
        [DataMember(Name = "stateCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateCode")]
        public string StateCode { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        /// <value>Сообщение</value>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}