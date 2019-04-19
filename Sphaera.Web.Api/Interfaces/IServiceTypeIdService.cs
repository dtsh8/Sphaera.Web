using System.Threading.Tasks;
//using JetBrains.Annotations;

namespace Sphaera.Web.Api.Interfaces
{
    /// <summary>
    /// Сервис для получения идентификатора типа службы.
    /// </summary>
    //[PublicAPI]
    public interface IServiceTypeIdService
    {
        /// <summary>
        /// Возвращает текущий идентификатор типа службы.
        /// </summary>
        Task<long> GetCurrentServiceTypeId();
    }
}
