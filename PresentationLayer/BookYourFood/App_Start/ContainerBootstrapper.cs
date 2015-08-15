using System;
using System.Web;
using ApplicationUserDomain.Infrastructure;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ReservationDomain.Infrastructure;

namespace BookYourFood.App_Start
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        private ContainerBootstrapper(IWindsorContainer container)
        {
            Container = container;
        }

        public IWindsorContainer Container { get; }

        public void Dispose()
        {
            Container.Dispose();
        }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer()
                .Install(
                    FromAssembly.This(),
                    new ApplicationUserBC.Configuration.Startup());

            container.Register(Classes.FromAssemblyInThisApplication()
                .Where(type => type.IsPublic)
                .WithService.DefaultInterfaces().LifestyleTransient());

            container.Register(
                Component.For<ReservationDomainDbContext>()
                    .LifestylePerWebRequest()
                    .Named("ReservationDomainDbContextDefault")
                    .IsDefault(),
                Component.For<ApplicationSignInManager>()
                    .Named("ApplicationSignInManagerDefault")
                    .IsDefault().LifestyleTransient(),
                Component.For<ApplicationUserManager>()
                    .Named("ApplicationUserManagerDefault")
                    .IsDefault()
                    .LifestyleTransient(),
                Component.For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .Named("IAuthenticationManagerDefault")
                    .IsDefault()
                    .LifestyleTransient(),
                Component.For<IUserStore<ApplicationUser>>()
                    .ImplementedBy<UserStore<ApplicationUser>>()
                    .UsingFactoryMethod(
                        () => new UserStore<ApplicationUser>(container.Resolve<ApplicationUserDomainDbContext>()))
                    .Named("UserStoreDefault")
                    .IsDefault()
                    .LifestyleTransient()
                );

            return new ContainerBootstrapper(container);
        }
    }
}