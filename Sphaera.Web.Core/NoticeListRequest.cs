using Sphaera.Web.Core.Enum;

namespace Sphaera.Web.Core
{
    public class NoticeListRequest
    {
        public string IncidentId { get; set; }

        public string CardId { get; set; }

        public NoticeType NoticeType { get; set; }
    }
}
