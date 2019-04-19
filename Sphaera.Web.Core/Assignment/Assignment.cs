using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Assignment
{
    /// <summary>
    /// Поручение
    /// </summary>
    [DataContract]
    public class Assignment
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }

        /// <summary>
        /// Идентификатор карточки
        /// </summary>
        [DataMember(Name = "cardId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }

        /// <summary>
        /// Идентификатор шаблона
        /// </summary>
        [DataMember(Name = "templateId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "templateId")]
        public long? TemplateId { get; set; }

        /// <summary>
        /// Название шаблона
        /// </summary>
        [DataMember(Name = "template", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "template")]
        public string Template { get; set; }

        /// <summary>
        /// Идентификатор состояния поручения
        /// </summary>
        [DataMember(Name = "stateId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateId")]
        public long StateId { get; set; }

        /// <summary>
        /// Код состояния поручения
        /// </summary>
        [DataMember(Name = "stateCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateCode")]
        public string StateCode { get; set; }

        /// <summary>
        /// Наименование состояния поручения
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Текст поручения
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Кем создано поручение
        /// </summary>
        [DataMember(Name = "createdBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Дата/время создания поручения
        /// </summary>
        [DataMember(Name = "created", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Срок исполнения поручения
        /// </summary>
        [DataMember(Name = "validTill", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "validTill")]
        public DateTime ValidTill { get; set; }

        /// <summary>
        /// Признак просрочки поручения
        /// </summary>
        [DataMember(Name = "overdue", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "overdue")]
        public bool Overdue { get; set; }
        
        /// <summary>
        /// Идентификатор родительского поручения
        /// </summary>
        [DataMember(Name = "parentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "parentId")]
        public long? ParentId { get; set; }
        
        /// <summary>
        /// Идентификатор ЕДДС
        /// </summary>
        [DataMember(Name = "dispatchServiceId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dispatchServiceId")]
        public int DispatchServiceId { get; set; }
        
        /// <summary>
        /// Наименование ЕДДС
        /// </summary>
        [DataMember(Name = "dispatchServiceName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dispatchServiceName")]
        public string DispatchServiceName { get; set; }
    }
}
