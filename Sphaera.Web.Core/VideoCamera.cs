using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Видео камера
    /// </summary>
    [DataContract]
    public class VideoCamera
    {
        /// <summary>
        /// Идентификатор объекта карты
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование камеры
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// URL видео потока
        /// </summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Широта установки камеры
        /// </summary>
        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота установки камеры
        /// </summary>
        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Адрес установки камеры
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Состояние камеры
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Тип камеры
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Направление
        /// </summary>
        [DataMember(Name = "direction", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "direction")]
        public double? Direction { get; set; }

        /// <summary>
        /// Угол обзора
        /// </summary>
        [DataMember(Name = "angle", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "angle")]
        public double? Angle { get; set; }

    }
}