using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Assignment
{
    /// <summary>
    /// Запрос изменения поручения
    /// </summary>
    [DataContract]
    public class SetAssignmentStateRequest
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        [DataMember(Name = "assignmentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "assignmentId")]
        public long AssignmentId { get; set; }

        /// <summary>
        /// Код состояние
        /// </summary>
        [DataMember(Name = "stateCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateCode")]
        public string StateCode { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [DataMember(Name = "dateChanged", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dateChanged")]
        public DateTime DateChanged { get; set; }

        /// <summary>
        /// Кем изменено
        /// </summary>
        [DataMember(Name = "changedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "changedBy")]
        public string ChangedBy { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}