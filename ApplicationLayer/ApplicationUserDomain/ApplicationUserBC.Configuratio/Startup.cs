using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationUserBC.Interfaces;
using ApplicationUserBC.Interfaces.DTOs;
using ApplicationUserDomain.Infrastructure;
using ApplicationUserDomain.Model.Repository;
using ApplicationUserDomain.Service;
using AutoMapper;
using BookYourFood;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace ApplicationUserBC.Configuration
{
    public class Startup : IWindsorInstaller
    {
        public Startup()
        {
            using (var byfDbContext = new ApplicationUserDomainDbContext())
            {
                byfDbContext.Database.Initialize(true);
            }
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Mapper.CreateMap<UserAnswer, UserAnswerDto>();

            Mapper.CreateMap<UserDto, ApplicationUser>();
            Mapper.CreateMap<ApplicationUser, UserDto>();


            container.Register(
                Component.For<ApplicationUserDomainDbContext>()
                    .LifestylePerWebRequest()
                    .Named("ApplicationUserDomainDbContextDefault").IsDefault(),
                Component.For<IApplicationUserService>()
                    .ImplementedBy<ApplicationUserService>()
                    .LifestyleTransient(),
                Component.For<IApplicationUserRepository>()
                    .ImplementedBy<ApplicationUserRepository>()
                    .LifestyleTransient(),
                Component.For<IUserStore<ApplicationUser>>()
                    .ImplementedBy<UserStore<ApplicationUser>>()
                    .UsingFactoryMethod(
                        () => new UserStore<ApplicationUser>(container.Resolve<ApplicationUserDomainDbContext>()))
                    .Named("UserStoreDefault")
                    .IsDefault()
                    .LifestyleTransient(),
                Component.For<ApplicationSignInManager.ApplicationUserManager>()
                    .Named("ApplicationUserManagerDefault")
                    .IsDefault()
                    .LifestylePerWebRequest(),
                Component.For<ApplicationSignInManager>()
                    .Named("ApplicationSignInManagerDefault")
                    .IsDefault().LifestyleTransient());
        }

        public static IDataProtectionProvider ConfigureAuth(IAppBuilder app)
        {
            var dataProtectionProvider = app.GetDataProtectionProvider();
            // Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationSignInManager.ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
            /*var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = "436221759874203",
                AppSecret = "e578a3a4a16a560155a76035b3168624"
            };
            facebookAuthenticationOptions.Scope.Add("email");
            facebookAuthenticationOptions.Scope.Add("public_profile");

            app.UseFacebookAuthentication(facebookAuthenticationOptions);*/
            /*app.UseFacebookAuthentication(
               appId: "436221759874203",
               appSecret: "e578a3a4a16a560155a76035b3168624");
            */
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            return dataProtectionProvider;
        }

    }
}