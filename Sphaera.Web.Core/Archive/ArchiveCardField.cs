namespace Sphaera.Web.Core.Archive
{
    /// <summary>
    /// Поля сортировки найденных архивных записей
    /// </summary>
    public enum ArchiveCardField
    {
        /// <summary>
        /// Номер карточки
        /// </summary>
        Number,
        /// <summary>
        /// Тип карточки (индекс)
        /// </summary>
        Type,
        /// <summary>
        /// Дата и время создания карточки
        /// </summary>
        Created,
        /// <summary>
        /// Краткий адрес
        /// </summary>
        Address,
        /// <summary>
        /// Признак наличия связанных поручений
        /// </summary>
        HasAssignment,
        /// <summary>
        /// Количество привлеченных ресурсов
        /// </summary>
        ResourcesCount
    }
}