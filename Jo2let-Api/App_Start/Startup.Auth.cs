using System;
using Jo2let.Api.Providers;
using Jo2let.Data;
using Jo2let.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Jo2let.Api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; }
        public static Func<UserManager<ApplicationUser>> UserManager { get; set; }
        public static RoleManager<IdentityRole> RoleManager { get; set; }

        static Startup()
        {
            PublicClientId = "self";

            UserManager = () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PropertyDbContext()));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new PropertyDbContext()));

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManager, RoleManager),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }



        public static string PublicClientId { get; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            try
            {
                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

                // Enable the application to use a cookie to store information for the signed in user
                // and to use a cookie to temporarily store information about a user logging in with a third party login provider
                //app.UseCookieAuthentication(new CookieAuthenticationOptions());
                //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

                // Enable the application to use bearer tokens to authenticate users
                app.UseOAuthBearerTokens(OAuthOptions);

                // Uncomment the following lines to enable logging in with third party login providers
                //app.UseMicrosoftAccountAuthentication(
                //    clientId: "",
                //    clientSecret: "");

                //app.UseTwitterAuthentication(
                //    consumerKey: "",
                //    consumerSecret: "");

                //app.UseFacebookAuthentication(
                //    appId: "",
                //    appSecret: "");

                //app.UseGoogleAuthentication();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}