using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Cards
{
    /// <summary>
    /// Индекс карточки события (Тип происшествия в ТЗ).
    /// </summary>
    [DataContract]
    public class CardIndex
    {
        /// <summary>
        /// Код индекса карточки события
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Идентификатор типа карточки. Он же serviceTypeId.
        /// </summary>
        [DataMember(Name = "caseTypeId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "caseTypeId")]
        public long? CaseTypeId { get; set; }

        /// <summary>
        /// Родительский код индекса карточки события
        /// </summary>
        [DataMember(Name = "parentCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "parentCode")]
        public string ParentCode { get; set; }

        /// <summary>
        /// Название индекса карточки события
        /// </summary>
        [DataMember(Name = "indexName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "indexName")]
        public string IndexName { get; set; }

        /// <summary>
        /// Полное название индекса карточки события
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание индекса карточки события
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер индекса карточки события
        /// </summary>
        [DataMember(Name = "orderNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "orderNo")]
        public int? OrderNo { get; set; }
    }
}