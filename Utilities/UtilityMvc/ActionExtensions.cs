using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Utility
{
    public static class ActionExtensions
    {
        public static bool ActionAuthorized(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            var controllerBase = string.IsNullOrEmpty(controllerName)
                ? htmlHelper.ViewContext.Controller
                : htmlHelper.GetControllerByName(controllerName);
            var controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);
            ControllerDescriptor controllerDescriptor =
                new ReflectedControllerDescriptor(controllerContext.Controller.GetType());
            var actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            if (actionDescriptor == null)
                return false;

            var filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));

            var authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);
            foreach (var authorizationFilter in filters.AuthorizationFilters)
            {
                authorizationFilter.OnAuthorization(authorizationContext);
                if (authorizationContext.Result != null)
                    return false;
            }
            return true;
        }

        public static ControllerBase GetControllerByName(this HtmlHelper htmlHelper, string controllerName)
        {
            var factory = ControllerBuilder.Current.GetControllerFactory();
            var controller = factory.CreateController(htmlHelper.ViewContext.RequestContext, controllerName);
            if (controller == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                    "The IControllerFactory '{0}' did not return a controller for the name '{1}'.", factory.GetType(),
                    controllerName));
            }
            return (ControllerBase) controller;
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, new RouteValueDictionary(),
                new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            object routeValues, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, new RouteValueDictionary(routeValues),
                new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            string controllerName, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, controllerName, new RouteValueDictionary(),
                new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            RouteValueDictionary routeValues, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, routeValues, new RouteValueDictionary(),
                showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, new RouteValueDictionary(routeValues),
                new RouteValueDictionary(htmlAttributes), showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes,
            bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, null, routeValues, htmlAttributes,
                showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            string controllerName, object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, controllerName,
                new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes),
                showActionLinkAsDisabled);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes,
            bool showActionLinkAsDisabled)
        {
            if (htmlHelper.ActionAuthorized(actionName, controllerName))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            if (showActionLinkAsDisabled)
            {
                var tagBuilder = new TagBuilder("span") {InnerHtml = linkText};
                return MvcHtmlString.Create(tagBuilder.ToString());
            }
            return MvcHtmlString.Empty;
        }
    }
}