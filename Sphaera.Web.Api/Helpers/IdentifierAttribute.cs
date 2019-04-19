using System;

namespace Sphaera.Web.Api.Helpers
{
    public class IdentifierAttribute : Attribute
    {
        public string Name { get; set; }
    }
}