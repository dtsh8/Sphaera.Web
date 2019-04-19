using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Приложенный файл
    /// </summary>
    [DataContract]
    public class File
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        /// <value>Имя файла</value>
        [DataMember(Name = "fileName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        /// <value>Содержимое файла</value>
        [DataMember(Name = "body", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "body")]
        public byte[] Body { get; set; }
    }
}