using Microsoft.Extensions.Configuration;

namespace Sphaera.Web.Api.Configuration
{
    public class ServiceTypeIdConfiguration : IServiceTypeIdConfiguration
    {
        public ServiceTypeIdConfiguration(long peoplePublicAreaServiceTypeId, long chiefPersonalAreaServiceTypeId)
        {
            PeoplePublicAreaServiceTypeId = peoplePublicAreaServiceTypeId;
            ChiefPersonalAreaServiceTypeId = chiefPersonalAreaServiceTypeId;
        }
        public ServiceTypeIdConfiguration(long peoplePublicAreaServiceTypeId, long chiefPersonalAreaServiceTypeId, long saveCityServiceTypeId)
            :this(peoplePublicAreaServiceTypeId, chiefPersonalAreaServiceTypeId)
        {
            SaveCityServiceTypeId = saveCityServiceTypeId;
        }

        public ServiceTypeIdConfiguration(IConfiguration configuration)
        {
            PeoplePublicAreaServiceTypeId = configuration.GetValue<long>(ConfigurationParameterNames.PeoplePublicAreaServiceTypeId);
            ChiefPersonalAreaServiceTypeId = configuration.GetValue<long>(ConfigurationParameterNames.ChiefPersonalAreaServiceTypeId);
            SaveCityServiceTypeId = configuration.GetValue<long>(ConfigurationParameterNames.SaveCityServiceTypeId);
        }

        /// <inheritdoc/>
        public long PeoplePublicAreaServiceTypeId { get; }

        /// <inheritdoc/>
        public long ChiefPersonalAreaServiceTypeId { get; }

        /// <inheritdoc/>
        public long SaveCityServiceTypeId { get; }
    }
}
