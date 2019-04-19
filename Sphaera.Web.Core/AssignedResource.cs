using System;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Ресурс назначенный на происшествие
    /// </summary>
    public class AssignedResource : Resource
    {
        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }
        
        /// <summary>
        /// Идентификатор карточки вызова службы реагирования
        /// </summary>
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }
        
        /// <summary>
        /// Идентификатор миссии ресурса реагирования
        /// </summary>
        [JsonProperty(PropertyName = "missionId")]
        public string MissionId { get; set; }
        
        /// <summary>
        /// Момент события
        /// </summary>
        [JsonProperty(PropertyName = "eventDate")]
        public DateTime EventDate { get; set; }
        
        /// <summary>
        /// Статус ресурса реагирования
        /// </summary>
        [JsonProperty(PropertyName = "resourceState")]
        public string ResourceState { get; set; }
    }
}
