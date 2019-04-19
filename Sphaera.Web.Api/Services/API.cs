using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sphaera.Web.Api.Services
{
    public static class API
    {
        //public static object Cards { get; internal set; }

        public static class Cards
        {
            public static string GetCardsUri(string baseUri, string search)
            {
                return $"{ baseUri }/api/v1/Address/Find?searchString={search}";
            }

            public static string GetSubscribeUri(string baseUri, string cardUri)
            {
                return $"{baseUri}/{cardUri}";
            }
        }


        public static class Address
        {
            public static string GetAddresses(string baseUri, string search)
            {
                return $"{ baseUri }/api/v1/Address/Find?searchString={search}";
            }

            //public static string GetLocality(string baseUri, string municipalityFiasCode, string search)
            //{
            //    var queryString = ""
            //    .AddQueryParameter("municipalityFiasCode", municipalityFiasCode)
            //    .AddQueryParameter("searchString", search);

            //    return $"{baseUri}/api/v1/Address/FindLocality?{queryString}";
            //}
        }
    }
}
