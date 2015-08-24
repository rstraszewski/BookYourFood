using System;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
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
                
                
                Component.For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .Named("IAuthenticationManagerDefault")
                    .IsDefault()
                    .LifestyleTransient(),
                
                Component.For<IDataProtectionProvider>()
                    .UsingFactoryMethod(() => Startup.DataProtectionProvider)
                );

            return new ContainerBootstrapper(container);
        }
    }
}