using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Sphaera.Web.Core
{
    /// <summary>
    /// Запрос на реагирование
    /// </summary>
    [DataContract]
    public class IncidentClaim
    {
        /// <summary>
        /// Логин заявителя
        /// </summary>
        /// <value>Логин заявителя</value>
        [DataMember(Name = "submitterLogin", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "submitterLogin")]
        public string SubmitterLogin { get; set; }

        /// <summary>
        /// Имя заявителя
        /// </summary>
        /// <value>Имя заявителя</value>
        [DataMember(Name = "submitterName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "submitterName")]
        public string SubmitterName { get; set; }

        /// <summary>
        /// Фамилия заявителя
        /// </summary>
        /// <value>Фамилия заявителя</value>
        [DataMember(Name = "submitterLastName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "submitterLastName")]
        public string SubmitterLastName { get; set; }

        /// <summary>
        /// Отчество заявителя
        /// </summary>
        /// <value>Отчество заявителя</value>
        [DataMember(Name = "submitterParental", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "submitterParental")]
        public string SubmitterParental { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        /// <value>Телефон</value>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        /// <value>Адрес электронной почты</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Подписка на SMS информирование
        /// </summary>
        /// <value>Подписка на SMS информирование</value>
        [DataMember(Name = "smsSubscribe", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "smsSubscribe")]
        public bool? SmsSubscribe { get; set; }

        /// <summary>
        /// Подписка на информирование по электронной почте
        /// </summary>
        /// <value>Подписка на информирование по электронной почте</value>
        [DataMember(Name = "emailSubscribe", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "emailSubscribe")]
        public bool? EmailSubscribe { get; set; }

        /// <summary>
        /// Текст заявки
        /// </summary>
        /// <value>Текст заявки</value>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}