using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    public interface INoticeService
    {
        Task<Notice[]> GetList(NoticeListRequest noticeListRequest);

        Task<bool> Update(Notice data);
    }

    [UsedImplicitly]
    public class NoticeService : SeviceBaseSimple, INoticeService
    {
        private const string GetNoticeListUri = "/api/v1/Notice/Get?incidentId={0}&cardId={1}&noticeType={2}";
        private const string PutNoticeUri = "/api/v1/Notice/Put";

        public NoticeService([NotNull] IConfiguration config)
        {
            SvcUrl = config["NoticeSvc_URI"];
        }

        public async Task<Notice[]> GetList(NoticeListRequest noticeListRequest)
        {
            return await GetNotices(noticeListRequest);
        }

        public async Task<bool> Update(Notice data)
        {
            return await base.Update<Notice, bool>(PutNoticeUri, data);
        }

        private async Task<Notice[]> GetNotices(NoticeListRequest noticeListRequest)
        {
            return await base.GetList<Notice>(string.Format(GetNoticeListUri, noticeListRequest.IncidentId, noticeListRequest.CardId,
                (int)noticeListRequest.NoticeType));
        }
    }
}