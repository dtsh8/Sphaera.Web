using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sphaera.IdentityServer
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "3",
                    Username = "tom",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://tom.com")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                // OpenID Connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AllowOfflineAccess = true
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:44358/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:44358/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:44358" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
                //new Client
                //{
                //    ClientId = "angular_spa",
                //    ClientName = "Angular 4 Client",
                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,
                //    RequireClientSecret = false,
                //    AllowedScopes = new List<string> {"openid", "profile", "api1"},
                //    RedirectUris = new List<string> {"http://localhost:4200/auth-callback", "http://localhost:4200/silent-refresh.html"},
                //    PostLogoutRedirectUris = new List<string> {"http://localhost:4200/"},
                //    AllowedCorsOrigins = new List<string> {"http://localhost:4200"},
                //    AllowAccessTokensViaBrowser = true
                //},
                new Client
                {
                    ClientId = "react_ts",
                    ClientName = "Typescript react Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = new List<string> {"openid", "profile", "api1"},
                    RedirectUris = new List<string> { "https://localhost:44301/signin-callback.html", "https://localhost:44301/silent-renew.html"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:44301/"},
                    AllowedCorsOrigins = new List<string> {"https://localhost:44301"},
                    AllowAccessTokensViaBrowser = true
                }

            };
        }
    }
}
