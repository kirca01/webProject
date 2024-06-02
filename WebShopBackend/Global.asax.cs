using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebShopBackend.Models;

namespace WebShopBackend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            List<User> users = Data.ReadUsers("~/App_Data/users.txt");
            HttpContext.Current.Application["users"] = users;

            List<Product> products = Data.ReadProducts("~/App_Data/products.txt");
            HttpContext.Current.Application["products"] = products;

            List<Order> orders = Data.ReadOrders("~/App_Data/orders.txt");
            HttpContext.Current.Application["orders"] = orders;

            List<Review> reviews = Data.ReadReviews("~/App_Data/reviews.txt");
            HttpContext.Current.Application["reviews"] = reviews;
        }
    }
}
