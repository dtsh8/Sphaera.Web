using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using JetBrains.Annotations;
using Sphaera.Web.Api;
using Sphaera.Web.Api.Extensions;
using Sphaera.Web.Core;

namespace Sphaera.Web.Models
{
    public class ApplicationUser
    {
        public ApplicationUser([NotNull] ClaimsPrincipal principal)
        {
            UserName = principal.Identity.Name;
            Id = principal.Identity.GetId();
            OrganizationCode = principal.Identity.GetOrgCode();
            MapExtent = principal.Identity.GetMapExtent();
            Email = principal.Claims.First(x => x.Type == JwtClaimTypes.Email).Value;
            EmailConfirmed = bool.Parse(principal.Claims.First(x => x.Type == JwtClaimTypes.EmailVerified).Value);
            PhoneNumber = principal.Claims.First(x => x.Type == JwtClaimTypes.PhoneNumber).Value;
            PhoneNumberConfirmed = bool.Parse(principal.Claims.First(x => x.Type == JwtClaimTypes.PhoneNumberVerified).Value);
            LastName = principal.Claims.First(x => x.Type == JwtClaimTypes.FamilyName).Value;
            FirstName = principal.Claims.First(x => x.Type == JwtClaimTypes.GivenName).Value;
            MiddleName = principal.Claims.First(x => x.Type == JwtClaimTypes.MiddleName).Value;
            Role = principal.Claims.First(x => x.Type == Constants.ClaimTypes.Role).Value;
        }

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string OrganizationCode { get; set; }

        public string MapExtent { get; set; }

        public string Role { get; set; }

        public DispatchService[] DispatchServices { get; set; }
    }
}