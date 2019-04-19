using System;

namespace Sphaera.Web.Core.Archive
{
    /// <summary>
    /// Архивная карточка реагирования
    /// </summary>
    public class ArchiveCard
    {
        /// <summary>
        /// Идентификатор происшествия
        /// </summary>
        public string IncidentId { get; set; }
        /// <summary>
        /// Идентификатор карточки вызова службы реагирования
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// Идентификатор статуса карточки
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// Название статуса карточки
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// Код индекса карточки
        /// </summary>
        public string CardIndexCode { get; set; }
        /// <summary>
        /// Название индекса карточки
        /// </summary>
        public string CardIndexName { get; set; }
        /// <summary>
        /// Идентификатор типа службы реагирования
        /// </summary>
        public int ServiceTypeId { get; set; }
        /// <summary>
        /// Название типа службы реагирования
        /// </summary>
        public string ServiceTypeName { get; set; }
        /// <summary>
        /// Комментарий к карточке
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Момент создания карточки
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Момент закрытия карточки
        /// </summary>
        public DateTime Finished { get; set; }
        /// <summary>
        /// Широта места происшествия
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// Долгота места происшествия
        /// </summary>
        public double? Longitude { get; set; }
        /// <summary>
        /// Краткий адрес происшествия
        /// </summary>
        public string ShortAddress { get; set; }
        /// <summary>
        /// Идентификатор ЕДДС
        /// </summary>
        public int DispatchServiceId { get; set; }
        /// <summary>
        /// Наименование ЕДДС
        /// </summary>
        public string DispatchServiceName { get; set; }
    }
}