using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sphaera.Web.Api
{
    public static class Constants
    {
        public const string DdMMyyyy = @"{0:dd.MM.yyyy}";
        public const string StrDdMMyyyyHHmm = @"dd.MM.yyyy HH:mm";
        public const string StrDdMMyyyy = @"dd.MM.yyyy";
        public const string StrDdMMMM = "dd MMMM";
        public const string StrDd = "dd";
        public const string InvariantDateTime = @"dd/MM/yyyy hh:mm";
        public const string N = @"{0:N0}";
        public const string N1 = @"{0:N1}";
        public const string N2 = @"{0:N2}";
        public const string N3 = @"{0:N3}";
        public const string D3 = @"{0:D3}";
        public const string D4 = @"{0:D4}";
        public const string C2 = @"{0:C2}";
        public const string C3 = @"{0:C3}";

        public static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        public static readonly DateTime MaxDate = new DateTime(2100, 1, 1);

        public const string KbkExpr = @"^\d{20}$";

        public const decimal MaxSumEx = 99999999999999;
        public const decimal MaxSum = 99999999999;
        public const decimal MinSum = -99999999999;
        public const int MaxCount = 99999;

        public const int MaxFileSize = 2147483646;

        public const string ExcelFormatN2 = "# ### ### ### ### ##0.00";
        public const string ExcelFormatN10 = "# ### ### ### ### ##0.0000000000";

        public const string PercentFormat = "{0:f2} %";

        public class ClaimTypes
        {
            public const string AccessToken = "access_token";
            public const string RefreshToken = "refresh_token";
            public const string ClientId = "client_id";
            public const string Id = "id";
            public const string Name = "name";
            public const string GivenName = "given_name";
            public const string FamilyName = "family_name";
            public const string MiddleName = "middle_name";
            public const string NickName = "nickname";
            public const string EMail = "email";
            public const string Phone = "phone_number";
            public const string Role = "role";
            public const string Company = "org_code";
            public const string Locked = "locked";
            public const string ExpiresAt = "expires_at";
            public const string Expires = "exp";
            public const string MapExtent = "map_extent";
            public const string DispatchServicesIds = "dispatch_service_ids";
        }

        public const string RoutePrefix = "api";
        public const string UserRoutePrefix = RoutePrefix + "/users";
        public const string RoleRoutePrefix = RoutePrefix + "/roles";

        public class RouteNames
        {
            public const string GetUsers = UserRoutePrefix;
            public const string GetUsersInfo = UserRoutePrefix + "/info";
            public const string GetUser = UserRoutePrefix + "/{0}";
            public const string GetUserName = UserRoutePrefix + "/{0}?app=false";
            public const string EditUserApp = UserRoutePrefix + "/{0}";
            public const string CreateUser = UserRoutePrefix + "?password={0}";
            public const string DeleteUser = UserRoutePrefix + "/{0}/delete/{1}";
            public const string EditClaim = UserRoutePrefix + "/{0}/property/{1}";
            public const string GetAuthUser = UserRoutePrefix + "/{0}/user/{1}?password={2}";
            public const string ChangePassword = UserRoutePrefix + "/{0}/password/{1}?password={2}";
        }
    }
}
