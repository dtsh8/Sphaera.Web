using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Внешняя организация службы реагирования
    /// </summary>
    [DataContract]
    public class Organization
    {
        /// <summary>
        /// Код организации службы реагирования
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование организации службы реагирования
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Экстент карты организации службы реагирования
        /// </summary>
        [DataMember(Name = "mapExtent", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "mapExtent")]
        public string MapExtent { get; set; }

        /// <summary>
        /// Идентификатор типа службы реагирования
        /// </summary>
        [DataMember(Name = "serviceTypeId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeId")]
        public long ServiceTypeId { get; set; }
    }
}