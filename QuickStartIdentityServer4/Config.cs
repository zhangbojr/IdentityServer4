using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuickStartIdentityServer4
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
       name: "custom.profile",
       displayName: "Custom profile",
       claimTypes: new[] { "name", "email", "status" });

            return new List<IdentityResource>
           {
               new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
               new IdentityResources.Profile(),
               new IdentityResources.Phone(),
               new IdentityResources.Email(),
               customProfile
           };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "alice",
            Password = "123",
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
            Password = "123",
             Claims = new []
            {
                new Claim("name", "Bob"),
                new Claim("website", "https://bob.com")
            }
        }
    };
        }

        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
           {
               new ApiResource("webApi", "My API")
           };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
           {
                //服务之间凭证认证
               new Client
               {
                   ClientId = "client1",
                   AllowedGrantTypes = GrantTypes.ClientCredentials,

                   ClientSecrets =
                   {
                       new Secret("secret".Sha256())
                   },
                   AllowedScopes = { "webApi",IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报forbidden错误
                 IdentityServerConstants.StandardScopes.Profile},
               },
               //使用密码
                 new Client
               {
                   ClientId = "client2",
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                   ClientSecrets =
                   {
                       new Secret("secret".Sha256())
                   },
                   AllowedScopes = { "webApi"},
               },
                    new Client
               {
                   //使用Hybrid Flow并添加API访问控制
                   ClientId = "mvc",
                   //AllowedGrantTypes = GrantTypes.Implicit,
                   AllowedGrantTypes = GrantTypes.Hybrid, //混合验证
                   ClientName="mvc client",
                   //登录成功后回调地址，或者回调数据
                   RedirectUris={"http://localhost:5003/signin-oidc" },
                   //退出地址
                   PostLogoutRedirectUris={"http://localhost:5003/signout-callback-oidc" },
                    ClientSecrets =
                   {
                       new Secret("secret".Sha256())
                   },
                   //授权展示信息
                   AllowedScopes = new List<string>
                   {
                   IdentityServerConstants.StandardScopes.OpenId,
                   IdentityServerConstants.StandardScopes.Profile,
                   "webApi"
                   },
                   //AllowOfflineAccess 允许我们通过刷新令牌的方式来实现长期的API访问
                   AllowOfflineAccess=true

               },//Javascript 客户端
               new Client
               {
                   ClientId = "js",
                   AllowedGrantTypes = GrantTypes.Code,
                   ClientName="JavaScript Client",
                   //登录成功后回调地址，或者回调数据
                   RedirectUris={"http://localhost:8083/#/pages/home" },
                   //退出地址
                   PostLogoutRedirectUris={"http://localhost:8083/#/pages/logout" },
                   AllowedCorsOrigins ={ "http://localhost:8083" },
                   //授权展示信息
                   RequirePkce=false,
                   RequireClientSecret=false,
                   AllowedScopes = new List<string>
                   {
                   IdentityServerConstants.StandardScopes.OpenId,
                   IdentityServerConstants.StandardScopes.Profile,
                   "webApi"
                   }
               }
           };
        }
    }
}
