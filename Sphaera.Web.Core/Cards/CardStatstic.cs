using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Cards
{
    /// <summary>
    /// Статистический показатель
    /// </summary>
    [DataContract]
    public class CardStatstic
    {
        /// <summary>
        /// Тип статистического показателя
        /// </summary>
        /// <value>Тип статистического показателя</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "type")]
        public int? Type { get; set; }

        [DataMember(Name = "periodType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "periodType")]
        public int? PeriodType { get; set; }
        
        [DataMember(Name = "periodCount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "periodCount")]
        public int? PeriodCount { get; set; }
        
        /// <summary>
        /// Значение статистического показателя
        /// </summary>
        /// <value>Значение статистического показателя</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "value")]
        public double? Value { get; set; }
    }
}