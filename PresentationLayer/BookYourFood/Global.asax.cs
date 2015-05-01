using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BookYourFood.Controllers;
using Database;
using ReservationDomain.Model;

namespace BookYourFood
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new NotificationActionFilterAttribute());
            //BundleTable.EnableOptimizations = true;
            using (var byfDbContext = new ByfDbContext())
            {
                byfDbContext.Database.Initialize(false);
            }
            RegisterMappings();
        }

        protected void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Table, TableViewModel>();
            AutoMapper.Mapper.CreateMap<TableViewModel, Table>();
            AutoMapper.Mapper.CreateMap<Reservation, ReservationSummaryViewModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CalculateCost(false)));
            AutoMapper.Mapper.CreateMap<MealForReservation, MealForSummary>()
                .ForMember(dest => dest.Name, opt=>opt.MapFrom(src => src.Meal.Name))
                .ForMember(dest => dest.NumberOfMeals, opt => opt.MapFrom(src => src.NumberOfMeals))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Meal.Price));
            AutoMapper.Mapper.CreateMap<DrinkForReservation, DrinkForSummary>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Drink.Name))
                .ForMember(dest => dest.NumberOfDrinks, opt => opt.MapFrom(src => src.NumberOfDrinks))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Drink.Price)); ;
        }
    }
}
