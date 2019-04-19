using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.AddressController
{
    /// <summary>
    /// Адрес места происшествия
    /// </summary>
    [DataContract]
    public class Address
    {
        /// <summary>
        /// Полный адрес
        /// </summary>
        [DataMember(Name = "fullAddress", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "fullAddress")]
        public string FullAddress { get; set; }

        /// <summary>
        /// Код ФИАС населенного пункта
        /// </summary>
        [DataMember(Name = "localityFias", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localityFias")]
        public string LocalityFias { get; set; }

        /// <summary>
        /// Код ФИАС улицы
        /// </summary>
        [DataMember(Name = "streetFias", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "streetFias")]
        public string StreetFias { get; set; }

        /// <summary>
        /// Код ФИАС дома
        /// </summary>
        [DataMember(Name = "houseFias", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "houseFias")]
        public string HouseFias { get; set; }

        /// <summary>
        /// Gets or Sets Region
        /// </summary>
        [DataMember(Name = "region", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// Муниципальное образование
        /// </summary>
        [DataMember(Name = "municipality", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "municipality")]
        public string Municipality { get; set; }

        /// <summary>
        /// Псевдоним муниципального образования
        /// </summary>
        [DataMember(Name = "municipalityAlias", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "municipalityAlias")]
        public string MunicipalityAlias { get; set; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        [DataMember(Name = "locality", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Gets or Sets Street
        /// </summary>
        [DataMember(Name = "street", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or Sets House
        /// </summary>
        [DataMember(Name = "house", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "house")]
        public string House { get; set; }

        /// <summary>
        /// Строение/корпус/владение
        /// </summary>
        [DataMember(Name = "building", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "building")]
        public string Building { get; set; }

        /// <summary>
        /// Gets or Sets Latitude
        /// </summary>
        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or Sets Longitude
        /// </summary>
        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Подъезд
        /// </summary>
        [DataMember(Name = "entrance", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "entrance")]
        public string Entrance { get; set; }

        /// <summary>
        /// Gets or Sets Floor
        /// </summary>
        [DataMember(Name = "floor", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "floor")]
        public string Floor { get; set; }

        /// <summary>
        /// Квартира/офис
        /// </summary>
        [DataMember(Name = "apartment", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "apartment")]
        public string Apartment { get; set; }

        /// <summary>
        /// Код подъезда
        /// </summary>
        [DataMember(Name = "entranceCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "entranceCode")]
        public string EntranceCode { get; set; }

        /// <summary>
        /// Шоссе
        /// </summary>
        [DataMember(Name = "highway", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "highway")]
        public Highway Highway { get; set; }

        /// <summary>
        /// Gets or Sets ObjectName
        /// </summary>
        [DataMember(Name = "objectName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "objectName")]
        public string ObjectName { get; set; }

        /// <summary>
        /// Gets or Sets Comment
        /// </summary>
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Признак общественного места
        /// </summary>
        [DataMember(Name = "publicPlace", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "publicPlace")]
        public bool? PublicPlace { get; set; }

        /// <summary>
        /// Время позиционирования
        /// </summary>
        [DataMember(Name = "positionTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "positionTime")]
        public DateTime? PositionTime { get; set; }
    }
}