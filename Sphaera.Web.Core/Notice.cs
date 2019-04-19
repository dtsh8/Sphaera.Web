using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Sphaera.Web.Core.Enum;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Запись
    /// </summary>
    [DataContract]
    public class Notice
    {
        /// <summary>
        /// Момент создания записи
        /// </summary>
        /// <value>Момент создания записи</value>
        [DataMember(Name = "created", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        /// <value>Идентификатор происшествия</value>
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }

        /// <summary>
        /// Идентификатор карточки происшествия
        /// </summary>
        /// <value>Идентификатор карточки происшествия</value>
        [DataMember(Name = "cardId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }

        /// <summary>
        /// Текст записи
        /// </summary>
        /// <value>Текст записи</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Автор записи
        /// </summary>
        /// <value>Автор записи</value>
        [DataMember(Name = "createdBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Порядковый номер записи
        /// </summary>
        /// <value>Порядковый номер записи</value>
        [DataMember(Name = "orderNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "orderNo")]
        public int? OrderNo { get; set; }

        /// <summary>
        /// Признак отмененной записи
        /// </summary>
        /// <value>Признак отмененной записи</value>
        [DataMember(Name = "canceled", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "canceled")]
        public bool Canceled { get; set; }

        /// <summary>
        /// Признак важности записи
        /// </summary>
        /// <value>Признак важности записи</value>
        [DataMember(Name = "important", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "important")]
        public bool Important { get; set; }

        /// <summary>
        /// Тип записи
        /// </summary>
        /// <value>Тип записи</value>
        [DataMember(Name = "noticeType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "noticeType")]
        public NoticeType NoticeType { get; set; }
    }
}