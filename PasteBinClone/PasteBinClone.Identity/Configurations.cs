using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace PasteBinClone.Identity
{
    public static class Configurations
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("PasteBinCloneAPi", "Web Api"),
            };


        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "PasteBinCloneAPi",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                         "PasteBinCloneAPi"
                    },

                    AccessTokenLifetime = 900,

                    RedirectUris = {"https://localhost:44306/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:44306/signout-callback-oidc" }
                }
            };
    }
}
