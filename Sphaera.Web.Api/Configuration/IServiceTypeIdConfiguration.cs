namespace Sphaera.Web.Api.Configuration
{
    public interface IServiceTypeIdConfiguration
    {
        /// <summary>
        /// Идентификатор типа службы портала граждан.
        /// </summary>
        long PeoplePublicAreaServiceTypeId { get; }

        /// <summary>
        /// Идентификатор типа службы портала руководителя.
        /// </summary>
        long ChiefPersonalAreaServiceTypeId { get; }
        
        /// <summary>
        /// Идентификатор типа службы безопасный город (происшествия).
        /// </summary>
        long SaveCityServiceTypeId { get; }
    }
}
