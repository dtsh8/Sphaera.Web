using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Пункт плана реагирования
    /// </summary>
    [DataContract]
    public class ReactionPlan
    {
        /// <summary>
        /// Инструкция пункта плана реагирования
        /// </summary>
        /// <value>Инструкция пункта плана реагирования</value>
        [DataMember(Name = "instruction", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "instruction")]
        public string Instruction { get; set; }

        /// <summary>
        /// Описание пункта плана реагирования
        /// </summary>
        /// <value>Описание пункта плана реагирования</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}