using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Cards
{
    /// <summary>
    /// Карточка вызова службы реагирования
    /// </summary>
    [DataContract]
    public class Card
    {
        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        /// <value>Идентификатор происшествия</value>
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }

        /// <summary>
        /// Идентификатор карточки вызова службы реагирования
        /// </summary>
        /// <value>Идентификатор карточки вызова службы реагирования</value>
        [DataMember(Name = "cardId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }

        /// <summary>
        /// Идентификатор статуса карточки
        /// </summary>
        /// <value>Идентификатор статуса карточки</value>
        [DataMember(Name = "stateId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateId")]
        public long StateId { get; set; }

        /// <summary>
        /// Название статуса карточки
        /// </summary>
        /// <value>Название статуса карточки</value>
        [DataMember(Name = "stateName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateName")]
        public string StateName { get; set; }

        /// <summary>
        /// Код индекса карточки
        /// </summary>
        /// <value>Код индекса карточки</value>
        [DataMember(Name = "cardIndexCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardIndexCode")]
        public string CardIndexCode { get; set; }

        /// <summary>
        /// Название индекса карточки
        /// </summary>
        /// <value>Название индекса карточки</value>
        [DataMember(Name = "cardIndexName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardIndexName")]
        public string CardIndexName { get; set; }

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
        /// Комментарий к карточке
        /// </summary>
        /// <value>Комментарий к карточке</value>
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Момент создания карточки
        /// </summary>
        /// <value>Момент создания карточки</value>
        [DataMember(Name = "created", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Признак чрезвычайной ситуации
        /// </summary>
        /// <value>Признак чрезвычайной ситуации</value>
        [DataMember(Name = "isDanger", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isDanger")]
        public bool? IsDanger { get; set; }

        /// <summary>
        /// Широта места происшествия
        /// </summary>
        /// <value>Широта места происшествия</value>
        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота места происшествия
        /// </summary>
        /// <value>Долгота места происшествия</value>
        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Признак отклонения от сроков реагирования
        /// </summary>
        /// <value>Признак отклонения от сроков реагирования</value>
        [DataMember(Name = "isOverdue", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isOverdue")]
        public bool? IsOverdue { get; set; }

        /// <summary>
        /// Просроченная операция
        /// </summary>
        /// <value>Просроченная операция</value>
        [DataMember(Name = "overdueOperation", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "overdueOperation")]
        public string OverdueOperation { get; set; }

        /// <summary>
        /// Краткий адрес происшествия
        /// </summary>
        /// <value>Краткий адрес происшествия</value>
        [DataMember(Name = "shortAddress", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "shortAddress")]
        public string ShortAddress { get; set; }

        /// <summary>
        /// Код станции/подстанции ресурсов реагирования
        /// </summary>
        /// <value>Код станции/подстанции ресурсов реагирования</value>
        [DataMember(Name = "stationCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stationCode")]
        public string StationCode { get; set; }

        /// <summary>
        /// Дата/время просмотра сообщений чата
        /// </summary>
        [JsonProperty(PropertyName = "chatReviewed")]
        public DateTime? ChatReviewed { get; set; }
        
        /// <summary>
        /// Признак наличия новых сообщений чата
        /// </summary>
        [JsonProperty(PropertyName = "newChatMessages")]
        public bool NewChatMessages { get; set; }

        /// <summary>
        /// Идентификатор ЕДДС
        /// </summary>
        [JsonProperty(PropertyName = "dispatchServiceId")]
        public int DispatchServiceId { get; set; }

        /// <summary>
        /// Наименование ЕДДС
        /// </summary>
        [JsonProperty(PropertyName = "dispatchServiceName")]
        public string DispatchServiceName { get; set; }

        /// <summary>
        /// Идентификатор миссии.
        /// </summary>
        [JsonProperty(PropertyName = "missionId")]
        public string MissionId => IncidentId + '|' + CardId;
    }
}