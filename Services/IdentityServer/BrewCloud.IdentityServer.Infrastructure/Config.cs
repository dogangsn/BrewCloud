﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace BrewCloud.IdentityServer.Infrastructure
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new[]
             {
            new ApiResource("resource_account"){Scopes = {"accountapi"}},
            new ApiResource("resource_vet"){Scopes = {"vetapi"}},
            new ApiResource("resource_integration"){Scopes={"integrationapi"}},
            new ApiResource("resource_mobile"){Scopes={"mobile"}},
            new ApiResource("resource_farm"){Scopes={"farmapi"}},
            new ApiResource("resource_chat"){Scopes={"chatapi"}},
            new ApiResource("resource_mail"){Scopes={"mailapi"}},
            new ApiResource("resource_gym"){Scopes={"gymapi"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };


        public static IEnumerable<IdentityResource> IdentityResources =>
             new IdentityResource[]
             {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
             };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("accountapi","Account Api"),
                new ApiScope("vetapi","Vet Api"),
                new ApiScope("integrationapi","Integration Api"),
                new ApiScope("mobile","Mobile Api"),
                new ApiScope("farmapi","Farm Api"),
                new ApiScope("chatapi","Chat Api"),
                new ApiScope("mailapi","Mail Api"),
                new ApiScope("gymapi","Gym Api"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
           new Client[]
           {
               new Client
               {
                   ClientName = "Api Client",
                   ClientId = "ApiClient",
                   ClientSecrets = { new Secret("secret".Sha256()) },
                   UpdateAccessTokenClaimsOnRefresh=true,
                   AllowedGrantTypes= GrantTypes.ClientCredentials,
                   AllowedScopes = { "accountapi", "vetapi", "integrationapi", "farmapi", "chatapi" , "mailapi", "gymapi", IdentityServerConstants.LocalApi.ScopeName }
               },
               new Client
               {
                   ClientName = "AdminAPI",
                   ClientId = "adminclient",
                   ClientSecrets = { new Secret("secret".Sha256()) },
                   AllowOfflineAccess = true,
                   AlwaysIncludeUserClaimsInIdToken =true,
                   AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                   AllowedScopes = { "accountapi", "vetapi", "integrationapi", "farmapi", "chatapi", "mailapi", "gymapi", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName },
                   AccessTokenLifetime=43200,
                   UpdateAccessTokenClaimsOnRefresh=true,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(30)- DateTime.Now).TotalSeconds,
                   RefreshTokenUsage= TokenUsage.ReUse
               },
               new Client
               {
                    ClientName="VetMobile",
                    ClientId="vetmobile",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowOfflineAccess=true,
                    AlwaysIncludeUserClaimsInIdToken =true,
                    UpdateAccessTokenClaimsOnRefresh=true,
                    AllowedGrantTypes={ "delegation" },
                    AllowedScopes={ "accountapi", "vetapi", "integrationapi", "mobile", "farmapi", "chatapi", "mailapi", "gymapi", IdentityServerConstants.LocalApi.ScopeName},
                    AccessTokenLifetime=43200,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(30)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                },
           };

    }
}