using System.Runtime.Serialization;
using Newtonsoft.Json;
using Sphaera.Web.Core.AddressController;
using Sphaera.Web.Core.Cards;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Происшествие
    /// </summary>
    [DataContract]
    public class Incident
    {
        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        /// <value>Идентификатор происшествия</value>
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }

        /// <summary>
        /// Запрос на реагирование
        /// </summary>
        /// <value>Запрос на реагирование</value>
        [DataMember(Name = "claim", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "claim")]
        public IncidentClaim Claim { get; set; }

        /// <summary>
        /// Адрес происшествия
        /// </summary>
        /// <value>Адрес происшествия</value>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Карточки реагирования
        /// </summary>
        /// <value>Карточки реагирования</value>
        [DataMember(Name = "cards", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cards")]
        public Card[] Cards { get; set; }

        /// <summary>
        /// Идентификатор карточки обращения пользователя.
        /// </summary>
        [JsonProperty(PropertyName = "userRequestCardId")]
        public string UserRequestCardId { get; set; }

        /// <summary>
        /// Идентификаторы карточек поручений.
        /// </summary>
        [JsonProperty(PropertyName = "assignmentsCardsId")]
        public string[] AssignmentsCardsId { get; set; }

        /// <summary>
        /// Идентификатор карточки происшествия
        /// </summary>
        [JsonProperty(PropertyName = "safeCityCardId")]
        public string SafeCityCardId { get; set; }
    }
}