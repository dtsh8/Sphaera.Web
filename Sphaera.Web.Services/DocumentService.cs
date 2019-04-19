using System;
using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Models.DocumentTree;

namespace Sphaera.Web.Services
{
    public interface IDocumentService
    {
        Task<DocumentTreeItemView[]> GetTreeItems();

        Task<DocumentTreeItem> Get(Guid id);
    }

    [UsedImplicitly]
    public class DocumentService : IDocumentService
    {
        private const string GetTreeItemsUri = "/api/DocumentTree/GetTreeItems";
        private const string GetUri = "/api/DocumentTree/Get";

        private readonly string _idmUrl;

        public DocumentService([NotNull] IConfiguration config)
        {
            _idmUrl = config["IdmInternalBaseAddress"];
        }

        public async Task<DocumentTreeItemView[]> GetTreeItems()
        {
            var idmProxy = new WebApiProxy(_idmUrl, true);
            return await idmProxy.GetResultAsync<DocumentTreeItemView[]>(GetTreeItemsUri);
        }

        public async Task<DocumentTreeItem> Get(Guid id)
        {
            var getParameters = HttpUtility.ParseQueryString("");
            getParameters["key"] = id.ToString();
            var idmProxy = new WebApiProxy(_idmUrl, true);
            return await idmProxy.GetResultAsync<DocumentTreeItem>($"{GetUri}?{getParameters}");
        }
    }
}