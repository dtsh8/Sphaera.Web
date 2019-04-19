using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Assignment
{
    /// <summary>
    /// Элемент истории по поручению
    /// </summary>
    [DataContract]
    public class AssignmentHistory
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор поручения
        /// </summary>
        [DataMember(Name = "assignmentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "assignmentId")]
        public long AssignmentId { get; set; }

        /// <summary>
        /// Идентификатор состояния
        /// </summary>
        [DataMember(Name = "stateId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateId")]
        public long StateId { get; set; }

        /// <summary>
        /// Код состояния
        /// </summary>
        [DataMember(Name = "stateCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateCode")]
        public long StateCode { get; set; }

        /// <summary>
        /// Состояние
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        [DataMember(Name = "stateChanged", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateChanged")]
        public DateTime StateChanged { get; set; }

        /// <summary>
        /// Кем изменено
        /// </summary>
        [DataMember(Name = "changeBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "changeBy")]
        public string ChangeBy { get; set; }
    }
}