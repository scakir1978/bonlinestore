using BOnlineStore.Shared;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BOnlineStore.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource(BOnlineStoreIdentityServerConstants.ApiResourcesDefinitions){Scopes={ BOnlineStoreIdentityServerConstants.ApiScopesDefinitionsFullPermission}},        
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(BOnlineStoreIdentityServerConstants.ApiScopesDefinitionsFullPermission),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "AngularClient",
                ClientName = "Angular Client",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:4200/callback", "http://localhost:4200/silent" },
                AllowedCorsOrigins={ "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200/callout" },
                FrontChannelLogoutUri = "http://localhost:4200/callout",
                AllowedScopes =
                {
                    //BOnlineStoreIdentityServerConstants.ApiScopesDefinitionsTenantId, 
                    BOnlineStoreIdentityServerConstants.ApiScopesDefinitionsFullPermission, 
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId, 
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess
                },
                AllowOfflineAccess = true,
                AccessTokenLifetime = ((60 * 60) * 6), // 6 saat
                RefreshTokenUsage = TokenUsage.ReUse,
                AbsoluteRefreshTokenLifetime = (((60 * 60) * 24) * 5 ), //5 gün                
            }
        };
}
