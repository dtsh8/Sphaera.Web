using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sphaera.IdentityServer.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorMessage Error { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId { get; set; }
    }
}
