using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Cards
{
    /// <summary>
    /// Тип карточки вызова службы реагирования
    /// </summary>
    [DataContract]
    public class CardType
    {
        /// <summary>
        /// Идентификатор типа карточки вызова службы реагирования
        /// </summary>
        /// <value>Идентификатор типа карточки вызова службы реагирования</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Название типа карточки вызова службы реагирования
        /// </summary>
        /// <value>Название типа карточки вызова службы реагирования</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}