namespace Sphaera.Web.Core
{
    /// <summary>
    /// Запрос списка ЕДДС
    /// </summary>
    public class DispatchServiceRequest
    {
        /// <summary>
        /// Код внешней организации (для портала служб)
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// Список идентификаторов ЕДДС доступных по правам (для портала руководителя)
        /// </summary>
        public long[] DispatchServiceIds { get; set; }
    }
}
