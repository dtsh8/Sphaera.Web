using Microsoft.Extensions.Configuration;

namespace Sphaera.Web.Api.Configuration
{
    public static class EmergencyAlertConfiguration
    {
        /// <summary>
        /// Возвращает индексы карточек чрезвычайных ситуаций.
        /// </summary>
        public static string[] GetEmergencyAlertCardIndexCodes(IConfiguration configuration)
        {
            return configuration.GetSection(ConfigurationParameterNames.EmergencyAlertCardIndexCodes).Get<string[]>();
        }
    }
}
