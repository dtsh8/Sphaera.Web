using Newtonsoft.Json;

namespace Sphaera.Web.Core.Assignment
{
    public class AssignmentFilePropertiesSetModel
    {
        [JsonProperty("incidentId")]
        public string IncidentId { get; set; }
        
        [JsonProperty("cardId")]
        public string CardId { get; set; }
        
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}