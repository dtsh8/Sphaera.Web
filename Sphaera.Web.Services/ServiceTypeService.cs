using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Services.Basics;

namespace Sphaera.Web.Services
{
    /// <summary>
    /// Справочник типов службы реагирования.
    /// </summary>
    public interface IServiceTypeService
    {
        /// <summary>
        /// Получение списка типов службы реагирования.
        /// </summary>
        Task<ServiceType[]> GetList();
    }

    /// <inheritdoc cref="IServiceTypeService"/>
    [UsedImplicitly]
    public class ServiceTypeService : SeviceBase<ServiceType>, IServiceTypeService
    {
        #region Private Consts

        private const string GetServiceTypeListUri = "/api/v1/CardType/Get";

        #endregion

        #region Constructor

        public ServiceTypeService([NotNull] IConfiguration config) : base(config)
        {
            // В внешнем сервисе диссонанс в наименовании - сервис CardType возвращает тип ServiceType.
            // У нас будем использовать ServiceType, как указано в карточке.
            SvcUrl = config["EmergencyCardType_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<ServiceType[]> GetList()
        {
            return await base.GetList(GetServiceTypeListUri, (cache, obj) => { cache.Add(obj.Id, obj); });
        }

        #endregion
    }
}