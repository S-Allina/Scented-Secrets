﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Shop.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> apiScopes =>
            new List<ApiScope> { 
                new ApiScope("shop", "Shop Server"),
                new ApiScope(name:"read", displayName:"Read your data."),
                new ApiScope(name:"write", displayName:"write your data."),
                new ApiScope(name:"delete", displayName:"delete your data.")


            };

        public static IEnumerable<Client> Clients=>
         new List<Client>
        {
            new Client
            {
                ClientId="client",
                ClientSecrets={new Secret("secret".Sha256())},
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                AllowedScopes={"read","write","profile"}
            },
            new Client
            {
                ClientId = "shop",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
								AllowedScopes =
				{
					  "shop",
					IdentityServerConstants.StandardScopes.OpenId,
					IdentityServerConstants.StandardScopes.Profile,
					IdentityServerConstants.StandardScopes.Email,

				},

				RedirectUris = { "https://localhost:7229/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7229/signout-callback-oidc" },

            },






        };
        
    }
}
