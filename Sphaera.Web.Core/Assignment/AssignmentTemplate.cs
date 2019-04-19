using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Assignment
{
    /// <summary>
    /// Шаблон поручения
    /// </summary>
    [DataContract]
    public class AssignmentTemplate
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Список файлов
        /// </summary>
        [DataMember(Name = "fileAliases", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "fileAliases")]
        public string[] FileAliases { get; set; }
    }
}