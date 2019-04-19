using System;

namespace Sphaera.Web.Core.Enum
{
    /// <summary>
    /// Тип статистического показателя (битовая маска)
    /// </summary>
    public enum StatsticType
    {
        /// <summary>
        /// Количество событий
        /// </summary>
        IncidentsCount,
        /// <summary>
        /// Количество событий в работе
        /// </summary>
        IncidentsOpenCount,
        /// <summary>
        /// Количество обработанных событий
        /// </summary>
        IncidentsFinishedCount,
        /// <summary>
        /// Количество зарегистрированных заявок
        /// </summary>
        IncidentsCreatedCount,
        /// <summary>
        /// Количество чрезвычайных событий
        /// </summary>
        DangerIncidentsCount
    }
}