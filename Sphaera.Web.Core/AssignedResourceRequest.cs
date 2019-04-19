namespace Sphaera.Web.Core
{
    /// <summary>
    /// Запрос ресурсов реагирования назначенных на происшествие/ассоциированных с карточкой реагирования
    /// </summary>
    public class AssignedResourceRequest
    {
        /// <summary>
        /// Массив идентификаторов происшествий для отбора назначенных ресурсов
        /// </summary>
        public string[] IncidentIds { get; set; }
        /// <summary>
        /// Массив идентификаторов миссий реагирования для отбора назначенных ресурсов
        /// </summary>
        public string[] MissionIds { get; set; }
    }
}
