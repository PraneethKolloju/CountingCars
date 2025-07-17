using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("auctionApp","Auction app full access")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
        //    new Client
        //    {
        //     ClientId = "postman",
        //     ClientName="Postman",
        //     AllowedScopes = {"openid", "profile", "auctionApp"},
        //     RedirectUris ={"https://www.getpostman.com/oauth2/callback"},
        //     ClientSecrets= new []{new Secret("NotASecret".Sha256())},
        //     AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
        //    },
            new Client
            {
                ClientId = "nextApp",
                ClientName = "nextApp",

                ClientSecrets = { new Secret("NotASecret".Sha256()) },

                // ✅ Allow both grant types
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials
                    .Union(GrantTypes.Code).ToList(),

                RequirePkce = true, // Needed for Next.js (PKCE)
                RequireClientSecret = true, // Needed for Postman (ROPC)

                RedirectUris = { "http://localhost:3000/api/auth/callback/id-server" },
                AllowOfflineAccess = true, // Enable refresh tokens

                AllowedScopes = { "auctionApp", "openid", "profile" },
                AccessTokenLifetime = 3600 * 24 * 30
            }
        };
}
