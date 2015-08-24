using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using BookYourFood.Controllers;
using BookYourFood.Models;
using ReservationDomain.Infrastructure;
using ReservationDomain.Model;
//using Database;

namespace BookYourFood
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new NotificationActionFilterAttribute());
            //BundleTable.EnableOptimizations = true;
            
           /* using (var byfDbContext = new ReservationDomainDbContext())
            {
                byfDbContext.Database.Initialize(true);
            }*/
            RegisterMappings();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        }

        protected void RegisterMappings()
        {
            Mapper.CreateMap<Table, TableViewModel>();
            Mapper.CreateMap<TableViewModel, Table>();
            Mapper.CreateMap<Reservation, ReservationSummaryViewModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CalculateCost(false)));
            Mapper.CreateMap<MealForReservation, MealForSummary>()
                .ForMember(dest => dest.Name, opt=>opt.MapFrom(src => src.Meal.Name))
                .ForMember(dest => dest.NumberOfMeals, opt => opt.MapFrom(src => src.NumberOfMeals))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Meal.Price));
            Mapper.CreateMap<DrinkForReservation, DrinkForSummary>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Drink.Name))
                .ForMember(dest => dest.NumberOfDrinks, opt => opt.MapFrom(src => src.NumberOfDrinks))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Drink.Price));
            Mapper.CreateMap<CustomMealViewModel, Meal>();
            Mapper.CreateMap<Meal, MealViewModel>()
                .AfterMap((src, dest) =>
                {
                    if(src.Image != null)
                    {
                        var imageBase64Data=Convert.ToBase64String(src.Image);
                        var imageDataUrl= string.Format("data:image/png;base64,{0}", imageBase64Data);
                        dest.ImageData = imageDataUrl;
                        
                    }
                });
        }
    }
}
