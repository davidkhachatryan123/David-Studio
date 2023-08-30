using System;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> ApiResource =>
            new ApiResource[]
            {
                new ApiResource("users", "Users Service")
                {
                    Scopes = { "users" }
                },
                new ApiResource("portfolio", "Portfolio Service")
                {
                    Scopes = { "portfolio" }
                },
                new ApiResource("pricing", "Pricing Service")
                {
                    Scopes = { "pricing" }
                },
                new ApiResource("storage", "Storage Service")
                {
                    Scopes = { "storage" }
                },
                new ApiResource("messenger", "Messenger Service")
                {
                    Scopes = { "messenger" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("users"),
                new ApiScope("portfolio"),
                new ApiScope("pricing"),
                new ApiScope("storage"),
                new ApiScope("messenger")
            };

        public static IEnumerable<Client> Clients(IConfiguration configuration) =>
            new Client[]
            {
                new Client
                {
                    ClientId = "crm",
                    ClientName = "CRM SPA OpenId Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Email,
                        "users", "portfolio", "pricing", "storage", "messenger"
                    },
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { $"{configuration["SpaClient"]}/" },
                    PostLogoutRedirectUris = { $"{configuration["SpaClient"]}/" },
                    AllowedCorsOrigins =     { $"{configuration["SpaClient"]}" },

                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    RequireClientSecret = false,
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "usersswaggerui",
                    ClientName = "Users Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["UsersApiClient"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["UsersApiClient"]}/swagger/" },

                    AllowedScopes ={ "users" }
                },
                new Client
                {
                    ClientId = "portfolioswaggerui",
                    ClientName = "Portfolio Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["PortfolioApiClient"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["PortfolioApiClient"]}/swagger/" },

                    AllowedScopes ={ "portfolio" }
                },
                new Client
                {
                    ClientId = "pricingswaggerui",
                    ClientName = "Pricing Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["PricingApiClient"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["PricingApiClient"]}/swagger/" },

                    AllowedScopes ={ "pricing" }
                },
                new Client
                {
                    ClientId = "storageswaggerui",
                    ClientName = "Storage Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["StorageApiClient"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["StorageApiClient"]}/swagger/" },

                    AllowedScopes ={ "storage" }
                },
                new Client
                {
                    ClientId = "messengerswaggerui",
                    ClientName = "Messenger Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["MessengerApiClient"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["MessengerApiClient"]}/swagger/" },

                    AllowedScopes ={ "messenger" }
                }
            };
    }
}