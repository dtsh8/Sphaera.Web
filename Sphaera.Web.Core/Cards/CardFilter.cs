using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.Cards
{

    /// <summary>
    /// Фильтр карточек реагирования
    /// </summary>
    [DataContract]
    public class CardFilter
    {
        /// <summary>
        /// Идентификаторы ЕДДС (если не указано - все)
        /// </summary>
        /// <value>Идентификаторы ЕДДС (если не указано - все)</value>
        [DataMember(Name = "dispatchServiceIds", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dispatchServiceIds")]
        public long[] DispatchServiceIds { get; set; }

        /// <summary>
        /// Идентификаторы типов служб (если не указано - все)
        /// </summary>
        /// <value>Идентификаторы типов служб (если не указано - все)</value>
        [DataMember(Name = "serviceTypeIds", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceTypeIds")]
        public long[] ServiceTypeIds { get; set; }

        /// <summary>
        /// Признак инверсии (для исключения) типов служб указанных в параметре ServiceTypeIds
        /// </summary>
        /// <value>Признак инверсии (для исключения) типов служб указанных в параметре ServiceTypeIds</value>
        [DataMember(Name = "exceptServiceTypeIds", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "exceptServiceTypeIds")]
        public bool ExceptServiceTypeIds { get; set; }

        /// <summary>
        /// Идентификатор происшествия (для поиска связанных карточек)
        /// </summary>
        /// <value>Идентификатор происшествия (для поиска связанных карточек)</value>
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public string IncidentId { get; set; }

        /// <summary>
        /// Идентификаторы статусов происшествий
        /// </summary>
        /// <value>Идентификаторы статусов происшествий</value>
        [DataMember(Name = "stateIds", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stateIds")]
        public long[] StateIds { get; set; }

        /// <summary>
        /// Признак инверсии (для исключения) статусов происшествий указанных в параметре StateIds
        /// </summary>
        /// <value>Признак инверсии (для исключения) статусов происшествий указанных в параметре StateIds</value>
        [DataMember(Name = "exceptStateIds", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "exceptStateIds")]
        public bool ExceptStateIds { get; set; }

        /// <summary>
        /// Код типа (индекса) происшествия
        /// </summary>
        /// <value>Код типа (индекса) происшествия</value>
        [DataMember(Name = "cardIndexCodes", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardIndexCodes")]
        public string[] CardIndexCodes { get; set; }

        /// <summary>
        /// Признак инверсии (для исключения) кодов типа (индекса) проишествий указанных в параметре CardIndexCodes
        /// </summary>
        /// <value>Признак инверсии (для исключения) кодов типа (индекса) проишествий указанных в параметре CardIndexCodes</value>
        [DataMember(Name = "exceptCardIndexCodes", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "exceptCardIndexCodes")]
        public bool ExceptCardIndexCodes { get; set; }

        /// <summary>
        /// Начало периода выборки карточек реагирования (для отчета)
        /// </summary>
        /// <value>Начало периода выборки карточек реагирования (для отчета)</value>
        [DataMember(Name = "createdFrom", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "createdFrom")]
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Окончание периода выборки карточек реагирования (для отчета)
        /// </summary>
        /// <value>Окончание периода выборки карточек реагирования (для отчета)</value>
        [DataMember(Name = "createdTill", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "createdTill")]
        public DateTime? CreatedTill { get; set; }

        /// <summary>
        /// Признак чрезвычайной ситуации
        /// </summary>
        /// <value>Признак чрезвычайной ситуации</value>
        [DataMember(Name = "isDanger", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isDanger")]
        public bool? IsDanger { get; set; }

        /// <summary>
        /// Широта точки привязки (для поиска по удалению от точки)
        /// </summary>
        /// <value>Широта точки привязки (для поиска по удалению от точки)</value>
        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота точки привязки (для поиска по удалению от точки)
        /// </summary>
        /// <value>Долгота точки привязки (для поиска по удалению от точки)</value>
        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Дистанция поиска (для поиска по удалению от точки)
        /// </summary>
        /// <value>Дистанция поиска (для поиска по удалению от точки)</value>
        [DataMember(Name = "distance", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "distance")]
        public double? Distance { get; set; }

        /// <summary>
        /// Строковое представление углов карты (southwest_lng,southwest_lat,northeast_lng,northeast_lat)
        /// </summary>
        /// <value>Строковое представление углов карты (southwest_lng,southwest_lat,northeast_lng,northeast_lat)</value>
        [DataMember(Name = "mapBounds", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "mapBounds")]
        public string MapBounds { get; set; }

        /// <summary>
        /// Признак отклонения от сроков реагирования
        /// </summary>
        /// <value>Признак отклонения от сроков реагирования</value>
        [DataMember(Name = "isOverdue", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isOverdue")]
        public bool? IsOverdue { get; set; }

        /// <summary>
        /// Код организации (для внешних служб реагирования)
        /// </summary>
        /// <value>Код организации (для внешних служб реагирования)</value>
        [DataMember(Name = "organizationCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "organizationCode")]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// Код станции/подстанции ресурсов реагирования (для внешних служб реагирования)
        /// </summary>
        /// <value>Код станции/подстанции ресурсов реагирования (для внешних служб реагирования)</value>
        [DataMember(Name = "stationCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "stationCode")]
        public string StationCode { get; set; }

        /// <summary>
        /// Логин пользователя (для обращений граждан)
        /// </summary>
        /// <value>Логин пользователя (для обращений граждан)</value>
        [DataMember(Name = "userLogin", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "userLogin")]
        public string UserLogin { get; set; }

        
        public void Clear(CardFilter filter)
        {
            filter.DispatchServiceIds = filter.DispatchServiceIds?.Length == 0 ? null : filter.DispatchServiceIds;
            filter.ServiceTypeIds = filter.ServiceTypeIds?.Length == 0 ? null : filter.ServiceTypeIds;
            filter.IncidentId = string.IsNullOrWhiteSpace(filter.IncidentId) ? null : filter.IncidentId;
            filter.StateIds = filter.StateIds?.Length == 0 ? null : filter.StateIds;
            filter.CardIndexCodes = filter.CardIndexCodes?.Length == 0 ? null : filter.CardIndexCodes;

            filter.IsDanger = filter.IsDanger.HasValue && filter.IsDanger.Value == false ? null : filter.IsDanger;
            filter.Latitude = filter.Latitude.HasValue && Math.Abs(filter.Latitude.Value) <= 0.001 ? null : filter.Latitude;
            filter.Longitude = filter.Longitude.HasValue && Math.Abs(filter.Longitude.Value) <= 0.001 ? null : filter.Longitude;
            filter.Distance = filter.Distance.HasValue && Math.Abs(filter.Distance.Value) <= 0.001 ? null : filter.Distance;

            filter.MapBounds = string.IsNullOrWhiteSpace(filter.MapBounds) ? null : filter.MapBounds;
            filter.IsOverdue = filter.IsOverdue.HasValue && filter.IsOverdue.Value == false ? null : filter.IsOverdue;
            filter.OrganizationCode = string.IsNullOrWhiteSpace(filter.OrganizationCode) ? null : filter.OrganizationCode;
            filter.StationCode = string.IsNullOrWhiteSpace(filter.StationCode) ? null : filter.StationCode;
            filter.UserLogin = string.IsNullOrWhiteSpace(filter.UserLogin) ? null : filter.UserLogin;
            //return filter;
        }
    }
}