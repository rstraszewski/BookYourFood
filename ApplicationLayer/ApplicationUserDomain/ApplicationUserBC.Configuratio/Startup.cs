using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUserBC.Interfaces.DTOs;
using ApplicationUserBC.Service;
using ApplicationUserDomain.Infrastructure;
using ApplicationUserDomain.Model.Repository;
using ApplicationUserDomain.Service;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Identity.Model;
using Microsoft.AspNet.Identity;

namespace ApplicationUserBC.Configuration
{
    public class Startup : IWindsorInstaller
    {
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
                    .LifestyleTransient());
        }
        
    }
}
