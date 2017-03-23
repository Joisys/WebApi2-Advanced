using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jo2let.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace Jo2let.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly Func<UserManager<ApplicationUser>> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationOAuthProvider(string publicClientId, 
                                        Func<UserManager<ApplicationUser>> userManager, 
                                        RoleManager<IdentityRole> roleManager)
        {
            _publicClientId = publicClientId;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                using (UserManager<ApplicationUser> userManager = _userManager())
                {
                    ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

                    if (user == null)
                    {
                        context.SetError("Invalid User Grant", "The user name or password is incorrect.");
                        return;
                    }

                    ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user,
                        context.Options.AuthenticationType);
                    ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user,
                        CookieAuthenticationDefaults.AuthenticationType);

                    var roleName = await GetRoleName(user.Roles.First().RoleId);
                    AuthenticationProperties properties = CreateProperties(user.UserName, roleName);
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(cookiesIdentity);
                }

            }
            catch (Exception)
            {

                throw new Exception("GrantResourceOwnerCredentials Failed! [ApplicationOAuthProvider]");
            }


        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, string role)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userName", userName},
                {"role", role}
            };
            return new AuthenticationProperties(data);
        }

        private async Task<string> GetRoleName(string roleId)
        {
            var result = await _roleManager.FindByIdAsync(roleId);
            return result.Name;
        }
    }
}