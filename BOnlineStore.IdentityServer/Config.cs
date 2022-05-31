using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BOnlineStore.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource(Constants.ApiResourcesDefinitions){Scopes={Constants.ApiScopesDefinitionsFullPermission}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            /*new IdentityResource(){Name=Constants.ApiScopesDefinitionsTenantId, DisplayName="Tenant Id", 
                UserClaims = new[]{Constants.ApiScopesDefinitionsTenantId}}*/
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(Constants.ApiScopesDefinitionsFullPermission),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            new ApiScope(Constants.ApiScopesDefinitionsTenantId)
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "AngularClient",
                ClientName = "Angular Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,            
                ClientSecrets = { new Secret("secret".Sha256()) },
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysSendClientClaims = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowedScopes = { Constants.ApiScopesDefinitionsTenantId, Constants.ApiScopesDefinitionsFullPermission, IdentityServerConstants.LocalApi.ScopeName }
            }
            
        };
}
