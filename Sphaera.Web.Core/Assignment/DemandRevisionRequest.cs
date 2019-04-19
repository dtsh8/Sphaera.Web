using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Assignment
{
    public class DemandRevisionRequest
    {
        /// <summary>
        /// Идентификатор поручения.
        /// </summary>
        [DataMember(Name = "assignmentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "assignmentId")]
        public long AssignmentId { get; set; }
        
        /// <summary>
        /// Текст.
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}