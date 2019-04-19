using System;

namespace Sphaera.Web.Core.Archive
{
    /// <summary>
    /// Фильтр архива карточек реагирования
    /// </summary>
    public class ArchiveCardFilter
    {
        /// <summary>
        /// Идентификаторы ЕДДС (если не указано - все)
        /// </summary>
        public int[] DispatchServiceIds { get; set; }
        /// <summary>
        /// Идентификаторы типов служб (если не указано - все)
        /// </summary>
        public long[] ServiceTypeIds { get; set; }
        /// <summary>
        /// Признак инверсии (для исключения) типов служб указанных в параметре ServiceTypeIds
        /// </summary>
        public bool ExceptServiceTypeIds { get; set; }
        /// <summary>
        /// Идентификаторы привлеченных типов служб
        /// </summary>
        public int[] InvitedServiceTypeIds { get; set; }
        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        public string IncidentId { get; set; }
        /// <summary>
        /// Код типа (индекса) происшествия
        /// </summary>
        public string[] CardIndexCodes { get; set; }
        /// <summary>
        /// Признак инверсии (для исключения) типов (индексов) происшествий указанных в параметре CardIndexCodes
        /// </summary>
        public bool ExceptCardIndexCodes { get; set; }
        /// <summary>
        /// Начало периода создания карточек реагирования
        /// </summary>
        public DateTime? CreatedFrom { get; set; }
        /// <summary>
        /// Окончание периода создания карточек реагирования
        /// </summary>
        public DateTime? CreatedTill { get; set; }
        /// <summary>
        /// Начало периода закрытия карточек реагирования
        /// </summary>
        public DateTime? FinishedFrom { get; set; }
        /// <summary>
        /// Окончание периода закрытия карточек реагирования
        /// </summary>
        public DateTime? FinishedTill { get; set; }
        /// <summary>
        /// Признак чрезвычайной ситуации
        /// </summary>
        public bool? IsDanger { get; set; }
        /// <summary>
        /// Подстрока адреса
        /// </summary>
        public string AddressSubstr { get; set; }
        /// <summary>
        /// Подстрока описания происшествия
        /// </summary>
        public string CommentSubstr { get; set; }
        /// <summary>
        /// Подстрока ФИО заявителя
        /// </summary>
        public string CallerSubstr { get; set; }
        /// <summary>
        /// Код назначенного ресурса
        /// </summary>
        public string ResourceCode { get; set; }
        /// <summary>
        /// Количество уникальных назначенных ресурсов
        /// </summary>
        public int? ResourceCount { get; set; }
        /// <summary>
        /// Подстрока телефона заявителя
        /// </summary>
        public string PhoneSubstr { get; set; }
        /// <summary>
        /// Признак наличия связанного поручения
        /// </summary>
        public bool? HasAssignment { get; set; }
        /// <summary>
        /// Максимальное количество возвращаемых архивных записей 
        /// </summary>
        public int MaxRowCount { get; set; }
        /// <summary>
        /// Сортировка найденных архивных записей
        /// </summary>
        public ArchiveCardOrder[] ArchiveOrder { get; set; }
    }
}