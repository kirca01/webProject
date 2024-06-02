using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBackend.Models;
using WebShopBackend.ViewModels;

namespace WebShopBackend.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToShoppingCart()
        {
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            Guid id = Guid.Parse(Request.QueryString["productId"]);
            Product product = products.FirstOrDefault(p => p.Id == id);
            List<Product> shoppingCart = Session["shoppingCart"] as List<Product>;
            if(shoppingCart == null)
            {
                shoppingCart = new List<Product>();
            }
            shoppingCart.Add(product);
            Session["shoppingCart"] = shoppingCart;

            return Json(shoppingCart, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveFromShoppingCart()
        {
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            Guid id = Guid.Parse(Request.QueryString["productId"]);
            Product product = products.FirstOrDefault(p => p.Id == id);
            List<Product> shoppingCart = Session["shoppingCart"] as List<Product>;
            shoppingCart.Remove(product);
            Session["shoppingCart"] = shoppingCart;

            return Json(shoppingCart, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddToFavorite()
        {
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            Guid id = Guid.Parse(Request.QueryString["productId"]);
            Product product = products.FirstOrDefault(p => p.Id == id);
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            List<User> users = (List<User>)HttpContext.Application["Users"];
            User user = users.FirstOrDefault(u => u.Username == username);
            if (user.FavoriteProducts.Contains(product.Id))
            {
                user.FavoriteProducts.Remove(product.Id);
            }
            else
            {
                user.FavoriteProducts.Add(product.Id);
            }
            Data.SaveUser("~/App_Data/users.txt", user);

            return Json(user.FavoriteProducts.Contains(product.Id), JsonRequestBehavior.AllowGet);
        }

        

    }
}