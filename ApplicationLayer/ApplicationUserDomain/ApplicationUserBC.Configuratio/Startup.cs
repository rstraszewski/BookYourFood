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
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

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
                Component.For<ApplicationUserManager>()
                    .Named("ApplicationUserManagerDefault")
                    .IsDefault()
                    .LifestylePerWebRequest(),
                Component.For<IUserStore<ApplicationUser>>()
                    .ImplementedBy<UserStore<ApplicationUser>>()
                    .UsingFactoryMethod(
                        () => new UserStore<ApplicationUser>(container.Resolve<ApplicationUserDomainDbContext>()))
                    .Named("UserStoreDefault")
                    .IsDefault()
                    .LifestyleTransient());
        }

        public class EmailService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                // Plug in your email service here to send an email.
                return Task.FromResult(0);
            }
        }

        public class SmsService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                // Plug in your SMS service here to send a text message.
                return Task.FromResult(0);
            }
        }

        public interface IApplicationUserManager
        {
            IPasswordHasher PasswordHasher { get; set; }
            IIdentityValidator<ApplicationUser> UserValidator { get; set; }
            IIdentityValidator<string> PasswordValidator { get; set; }
            IClaimsIdentityFactory<ApplicationUser, string> ClaimsIdentityFactory { get; set; }
            IIdentityMessageService EmailService { get; set; }
            IIdentityMessageService SmsService { get; set; }
            IUserTokenProvider<ApplicationUser, string> UserTokenProvider { get; set; }
            bool UserLockoutEnabledByDefault { get; set; }
            int MaxFailedAccessAttemptsBeforeLockout { get; set; }
            TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
            bool SupportsUserTwoFactor { get; }
            bool SupportsUserPassword { get; }
            bool SupportsUserSecurityStamp { get; }
            bool SupportsUserRole { get; }
            bool SupportsUserLogin { get; }
            bool SupportsUserEmail { get; }
            bool SupportsUserPhoneNumber { get; }
            bool SupportsUserClaim { get; }
            bool SupportsUserLockout { get; }
            bool SupportsQueryableUsers { get; }
            IQueryable<ApplicationUser> Users { get; }
            IDictionary<string, IUserTokenProvider<ApplicationUser, string>> TwoFactorProviders { get; }
            void Dispose();
            Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType);
            Task<IdentityResult> CreateAsync(ApplicationUser user);
            Task<IdentityResult> UpdateAsync(ApplicationUser user);
            Task<IdentityResult> DeleteAsync(ApplicationUser user);
            Task<ApplicationUser> FindByIdAsync(string userId);
            Task<ApplicationUser> FindByNameAsync(string userName);
            Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
            Task<ApplicationUser> FindAsync(string userName, string password);
            Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
            Task<bool> HasPasswordAsync(string userId);
            Task<IdentityResult> AddPasswordAsync(string userId, string password);
            Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
            Task<IdentityResult> RemovePasswordAsync(string userId);
            Task<string> GetSecurityStampAsync(string userId);
            Task<IdentityResult> UpdateSecurityStampAsync(string userId);
            Task<string> GeneratePasswordResetTokenAsync(string userId);
            Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
            Task<ApplicationUser> FindAsync(UserLoginInfo login);
            Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo login);
            Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
            Task<IList<UserLoginInfo>> GetLoginsAsync(string userId);
            Task<IdentityResult> AddClaimAsync(string userId, Claim claim);
            Task<IdentityResult> RemoveClaimAsync(string userId, Claim claim);
            Task<IList<Claim>> GetClaimsAsync(string userId);
            Task<IdentityResult> AddToRoleAsync(string userId, string role);
            Task<IdentityResult> AddToRolesAsync(string userId, params string[] roles);
            Task<IdentityResult> RemoveFromRolesAsync(string userId, params string[] roles);
            Task<IdentityResult> RemoveFromRoleAsync(string userId, string role);
            Task<IList<string>> GetRolesAsync(string userId);
            Task<bool> IsInRoleAsync(string userId, string role);
            Task<string> GetEmailAsync(string userId);
            Task<IdentityResult> SetEmailAsync(string userId, string email);
            Task<ApplicationUser> FindByEmailAsync(string email);
            Task<string> GenerateEmailConfirmationTokenAsync(string userId);
            Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
            Task<bool> IsEmailConfirmedAsync(string userId);
            Task<string> GetPhoneNumberAsync(string userId);
            Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber);
            Task<IdentityResult> ChangePhoneNumberAsync(string userId, string phoneNumber, string token);
            Task<bool> IsPhoneNumberConfirmedAsync(string userId);
            Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber);
            Task<bool> VerifyChangePhoneNumberTokenAsync(string userId, string token, string phoneNumber);
            Task<bool> VerifyUserTokenAsync(string userId, string purpose, string token);
            Task<string> GenerateUserTokenAsync(string purpose, string userId);

            void RegisterTwoFactorProvider(string twoFactorProvider,
                IUserTokenProvider<ApplicationUser, string> provider);

            Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId);
            Task<bool> VerifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token);
            Task<string> GenerateTwoFactorTokenAsync(string userId, string twoFactorProvider);
            Task<IdentityResult> NotifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token);
            Task<bool> GetTwoFactorEnabledAsync(string userId);
            Task<IdentityResult> SetTwoFactorEnabledAsync(string userId, bool enabled);
            Task SendEmailAsync(string userId, string subject, string body);
            Task SendSmsAsync(string userId, string message);
            Task<bool> IsLockedOutAsync(string userId);
            Task<IdentityResult> SetLockoutEnabledAsync(string userId, bool enabled);
            Task<bool> GetLockoutEnabledAsync(string userId);
            Task<DateTimeOffset> GetLockoutEndDateAsync(string userId);
            Task<IdentityResult> SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd);
            Task<IdentityResult> AccessFailedAsync(string userId);
            Task<IdentityResult> ResetAccessFailedCountAsync(string userId);
            Task<int> GetAccessFailedCountAsync(string userId);
        }

        public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
        {
            public ApplicationUserManager(IUserStore<ApplicationUser> store,
                IDataProtectionProvider dataProtectionProvider)
                : base(store)
            {
                // Configure validation logic for usernames
                UserValidator = new UserValidator<ApplicationUser>(this)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false
                };

                // Configure user lockout defaults
                UserLockoutEnabledByDefault = true;
                DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                MaxFailedAccessAttemptsBeforeLockout = 5;

                // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
                // You can write your own provider and plug it in here.
                RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
                {
                    MessageFormat = "Your security code is {0}"
                });
                RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });
                EmailService = new EmailService();
                SmsService = new SmsService();

                if (dataProtectionProvider != null)
                {
                    var dataProtector = dataProtectionProvider.Create("ASP.NET Identity");

                    UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector);
                }
            }
        }
    }
}