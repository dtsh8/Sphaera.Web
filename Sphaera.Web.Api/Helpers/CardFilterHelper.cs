using System;
using Sphaera.Web.Core;
using Sphaera.Web.Core.Cards;

namespace Sphaera.Web.Api.Helpers
{
    public static class CardFilterHelper
    {
        public static CardFilter ClearCardFilter(CardFilter filter)
        {
            filter.DispatchServiceIds = filter.DispatchServiceIds?.Length == 0 ? null : filter.DispatchServiceIds;
            filter.ServiceTypeIds = filter.ServiceTypeIds?.Length == 0 ? null : filter.ServiceTypeIds;
            filter.IncidentId = string.IsNullOrWhiteSpace(filter.IncidentId) ? null : filter.IncidentId;
            filter.StateIds = filter.StateIds?.Length == 0 ? null : filter.StateIds;
            filter.CardIndexCodes = filter.CardIndexCodes?.Length == 0 ? null : filter.CardIndexCodes;

            filter.IsDanger = filter.IsDanger.HasValue && filter.IsDanger.Value == false ? null : filter.IsDanger;
            filter.Latitude = filter.Latitude.HasValue && Math.Abs(filter.Latitude.Value) <= 0.001 ? null : filter.Latitude;
            filter.Longitude = filter.Longitude.HasValue && Math.Abs(filter.Longitude.Value) <= 0.001 ? null : filter.Longitude;
            filter.Distance = filter.Distance.HasValue && Math.Abs(filter.Distance.Value) <= 0.001 ? null : filter.Distance;

            filter.MapBounds = string.IsNullOrWhiteSpace(filter.MapBounds) ? null : filter.MapBounds;
            filter.IsOverdue = filter.IsOverdue.HasValue && filter.IsOverdue.Value == false ? null : filter.IsOverdue;
            filter.OrganizationCode = string.IsNullOrWhiteSpace(filter.OrganizationCode) ? null : filter.OrganizationCode;
            filter.StationCode = string.IsNullOrWhiteSpace(filter.StationCode) ? null : filter.StationCode;
            filter.UserLogin = string.IsNullOrWhiteSpace(filter.UserLogin) ? null : filter.UserLogin;
            return filter;
        }
    }
}
