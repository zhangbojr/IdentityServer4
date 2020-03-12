using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyIdentityServer4
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
           {
               new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
               new IdentityResources.Profile()
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
                    new Client
               {
                   //使用Hybrid Flow并添加API访问控制
                   ClientId = "mvc",
                   AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                   ClientName="mvc client",
                   //登录成功后回调地址，或者回调数据
                   RedirectUris={"http://localhost:5003/signin-oidc" },
                   //退出地址
                   PostLogoutRedirectUris={"http://localhost:5003/signout-callback-oidc" },
                   RequireConsent=false,
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
               }
           };
        }
    }
}
