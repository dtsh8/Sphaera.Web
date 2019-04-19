using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Изменение статуса ресурса реагирования во время миссии
    /// </summary>
    [DataContract]
    public class ResourceMissionStateChange
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

        /// <summary>
        /// Код миссии
        /// </summary>
        /// <value>Код миссии</value>
        [DataMember(Name = "missionId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "missionId")]
        public string MissionId { get; set; }

        /// <summary>
        /// Код внешней системы
        /// </summary>
        /// <value>Код внешней системы</value>
        [DataMember(Name = "externalSystem", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "externalSystem")]
        public string ExternalSystem { get; set; }
    }
}