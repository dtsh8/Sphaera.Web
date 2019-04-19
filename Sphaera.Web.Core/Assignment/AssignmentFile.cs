using Newtonsoft.Json;

namespace Sphaera.Web.Core.Assignment
{
    /// <summary>
    /// Файл приложенный к поручению.
    /// </summary>
    public class AssignmentFile
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Описание файла.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
