namespace Sphaera.Web.Core.Archive
{
    /// <summary>
    /// Результаты поиска по архиву
    /// </summary>
    public class ArchiveCards
    {
        /// <summary>
        /// Найденные архивные карточки
        /// </summary>
        public ArchiveCard[] Cards { get; set; }
        /// <summary>
        /// Признак наличия большего количества записей
        /// </summary>
        public bool MoreResults { get; set; }
    }
}
