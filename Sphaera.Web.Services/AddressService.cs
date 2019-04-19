using System;
using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Extensions;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Models.AddressController;
using Sphaera.Web.Server.Services.Basics;
using Municipality = Sphaera.Web.Server.Models.Municipality;

namespace Sphaera.Web.Services
{
    public interface IAddressService
    {
        Task<Address[]> Find(string search);
        Task<Locality[]> FindLocality(string municipalityFiasCode, string searchString);
        Task<Street[]> FindStreet(string municipalityFiasCode, Locality locality, string searchString);
    }

    [UsedImplicitly]
    public class AddressService : SeviceBaseSimple, IAddressService
    {
        #region Private Consts

        private const string AddressSearchUri = "/api/v1/Address/Find?searchString={0}";

        #endregion

        #region Constructor

        public AddressService([NotNull] IConfiguration config)
        {
            SvcUrl = config["AddressSearch_URI"];
        }

        #endregion

        #region Public Methods

        public async Task<Address[]> Find(string search)
        {
            return await base.GetList<Address>(string.Format(AddressSearchUri, Uri.EscapeDataString(search)));
        }

        public async Task<Locality[]> FindLocality(string municipalityFiasCode, string searchString)
        {
            var queryString = ""
                .AddQueryParameter("municipalityFiasCode", municipalityFiasCode)
                .AddQueryParameter("searchString", searchString);
            return await GetList<Locality>($"/api/v1/Address/FindLocality?{queryString}");
        }

        public async Task<Street[]> FindStreet(string municipalityFiasCode, Locality locality, string searchString)
        {
            var queryString = ""
                .AddQueryParameter("municipalityFiasCode", municipalityFiasCode)
                .AddQueryParameter("searchString", searchString);
            return await Set<Locality, Street[]>($"/api/v1/Address/FindStreet?{queryString}", locality);
        }

        #endregion
    }
}