using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    public class CreateIncidentResult
    {
        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        /// <value>Идентификатор происшествия</value>
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }

        /// <summary>
        /// Идентификатор карточки вызова службы реагирования
        /// </summary>
        /// <value>Идентификатор карточки вызова службы реагирования</value>
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }
    }
}
