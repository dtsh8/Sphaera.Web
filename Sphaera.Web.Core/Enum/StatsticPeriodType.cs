namespace Sphaera.Web.Core.Enum
{
    /// <summary>
    /// Тип периода сбора статистики
    /// </summary>
    public enum StatsticPeriodType
    {
        /// <summary>
        /// Всего
        /// </summary>
        Total,
        /// <summary>
        /// За последние N часов
        /// </summary>
        LastHours,
        /// <summary>
        /// За последние N дней
        /// </summary>
        LastDays,
        /// <summary>
        /// За текущие сутки
        /// </summary>
        CurrentDay,
        /// <summary>
        /// За последние N недель
        /// </summary>
        LastWeeks,
        /// <summary>
        /// За текущую неделю
        /// </summary>
        CurrentWeek,
        /// <summary>
        /// За последние N месяцев
        /// </summary>
        LastMonths,
        /// <summary>
        /// За текущий месяц
        /// </summary>
        CurrentMonth,
        /// <summary>
        /// За последние N лет
        /// </summary>
        LastYears,
        /// <summary>
        /// За текущий год
        /// </summary>
        CurrentYear
    }
}