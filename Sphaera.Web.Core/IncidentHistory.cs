using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// История реагирования по происшествию
    /// </summary>
    [DataContract]
    public class IncidentHistory
    {
        /// <summary>
        /// Идентификатор карточки реагирования
        /// </summary>
        /// <value>Идентификатор карточки реагирования</value>
        [DataMember(Name = "cardId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }

        /// <summary>
        /// Идентификатор типа службы реагирования
        /// </summary>
        /// <value>Идентификатор типа службы реагирования</value>
        [DataMember(Name = "serviceTypeId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeId")]
        public long ServiceTypeId { get; set; }

        /// <summary>
        /// Название типа службы реагирования
        /// </summary>
        /// <value>Название типа службы реагирования</value>
        [DataMember(Name = "serviceTypeName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeName")]
        public string ServiceTypeName { get; set; }

        /// <summary>
        /// Идентификатор статуса карточки реагирования
        /// </summary>
        /// <value>Идентификатор статуса карточки реагирования</value>
        [DataMember(Name = "stateId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateId")]
        public long StateId { get; set; }

        /// <summary>
        /// Название статуса карточки реагирования
        /// </summary>
        /// <value>Название статуса карточки реагирования</value>
        [DataMember(Name = "stateName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateName")]
        public string StateName { get; set; }

        /// <summary>
        /// Время установки статуса
        /// </summary>
        /// <value>Время установки статуса</value>
        [DataMember(Name = "stateChanged", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateChanged")]
        public DateTime StateChanged { get; set; }

        /// <summary>
        /// Пользователь изменивший статус
        /// </summary>
        /// <value>Пользователь изменивший статус</value>
        [DataMember(Name = "changedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "changedBy")]
        public string ChangedBy { get; set; }
    }
}