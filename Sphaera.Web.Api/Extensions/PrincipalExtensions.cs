using JetBrains.Annotations;
using Newtonsoft.Json;
using Sphaera.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Sphaera.Web.Api.Extensions
{
    public static class PrincipalExtensions
    {
        [PublicAPI]
        public static Guid GetId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.Id);
            if (claim == null)
                return Guid.Empty;

            Guid.TryParse(claim.Value, out var val);
            return val;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetOrgCode(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.Company);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetMapExtent(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.MapExtent);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetGivenName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.GivenName);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetMiddleName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.MiddleName);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetFamilyName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.FamilyName);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetUserNickName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.NickName);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetEMail(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.EMail);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetPhone(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.Phone);
            return claim?.Value;
        }

        [CanBeNull]
        [PublicAPI]
        public static List<long> GetDispatchServicesIds(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(Constants.ClaimTypes.DispatchServicesIds);
            string claimValue = claim?.Value;
            if (string.IsNullOrEmpty(claimValue))
                return null;

            var dispatchServicesId = JsonConvert.DeserializeObject<List<long>>(claimValue);
            return dispatchServicesId;
        }

        [PublicAPI]
        public static bool IsApproved(this IPrincipal principal)
        {
            if (principal?.Identity?.Name == null)
                return false;

            return principal.Identity.IsAuthenticated;
        }

        [PublicAPI]
        public static bool IsInRole(this IPrincipal principal, Role role)
        {
            if (!principal.IsApproved())
                return false;

            return ((ClaimsPrincipal)principal).Claims.Any(x => x.Type == Constants.ClaimTypes.Role && x.Value == role.ToString());
        }

        [PublicAPI]
        public static bool IsLocked(this IPrincipal principal)
        {
            var claim = ((ClaimsPrincipal)principal).FindFirst(Constants.ClaimTypes.Locked);
            if (string.IsNullOrEmpty(claim.Value))
                return false;
            if (DateTime.TryParse(claim.Value, out var dt))
                return dt < DateTime.Now;
            return false;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetAccessToken(this IPrincipal principal)
        {
            var cp = principal as ClaimsPrincipal;
            var tokenClaim = cp?.FindFirst(Constants.ClaimTypes.AccessToken);
            return tokenClaim?.Value;
        }

        [PublicAPI]
        public static Guid GetId(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return Guid.Empty;

            return GetId(principal.Identity);
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetLogin(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return principal?.Identity?.Name;
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetOrgCode(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return GetOrgCode(principal.Identity);
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetGivenName(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return GetGivenName(principal.Identity);
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetMiddleName(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return GetMiddleName(principal.Identity);
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetFamilyName(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return GetFamilyName(principal.Identity);
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetEMail(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return GetEMail(principal.Identity);
        }

        [CanBeNull]
        [PublicAPI]
        public static string GetPhone(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return GetPhone(principal.Identity);
        }

        [CanBeNull]
        [PublicAPI]
        public static List<long> GetDispatchServicesIds(this IPrincipal principal)
        {
            if (!principal.IsApproved())
                return null;

            return GetDispatchServicesIds(principal.Identity);
        }
    }
}
