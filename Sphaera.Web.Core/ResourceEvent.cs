using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Событие реагирования
    /// </summary>
    [DataContract]
    public class ResourceEvent
    {
        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        /// <value>Идентификатор происшествия</value>
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }

        /// <summary>
        /// Идентификатор карточки реагирования
        /// </summary>
        /// <value>Идентификатор карточки реагирования</value>
        [DataMember(Name = "cardId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }

        /// <summary>
        /// Идентификатор миссии реагирования
        /// </summary>
        /// <value>Идентификатор миссии реагирования</value>
        [DataMember(Name = "missionId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "missionId")]
        public string MissionId { get; set; }

        /// <summary>
        /// Момент события
        /// </summary>
        /// <value>Момент события</value>
        [DataMember(Name = "eventDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "eventDate")]
        public DateTime? EventDate { get; set; }

        /// <summary>
        /// Статус ресурса реагирования
        /// </summary>
        /// <value>Статус ресурса реагирования</value>
        [DataMember(Name = "resourceState", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "resourceState")]
        public string ResourceState { get; set; }
    }
}