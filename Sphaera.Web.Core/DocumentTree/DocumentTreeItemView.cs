using System;

namespace Sphaera.Web.Core.DocumentTree
{
    public class DocumentTreeItemView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? UpdateDate { get; set; }

        public Guid? ParentId { get; set; }

        public DocumentTreeItemType DocumentTreeItemType { get; set; }
    }
}
