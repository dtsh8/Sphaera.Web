namespace Sphaera.Web.Core.Archive
{
    /// <summary>
    /// Элемент сортировки найденных архивных записей
    /// </summary>
    public class ArchiveCardOrder
    {
        /// <summary>
        /// Поле сортировки найденных архивных записей
        /// </summary>
        public ArchiveCardField Field { get; set; }
        /// <summary>
        /// Признак обратной сортировки
        /// </summary>
        public bool DescentOrder { get; set; }
    }
}