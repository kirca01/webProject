using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBackend.Models;

namespace WebShopBackend.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            HttpCookie authCookie = Request.Cookies["AuthToken"];

            if (authCookie != null && !string.IsNullOrEmpty(authCookie["Username"]))
            {
                if (authCookie["Role"] == Role.ADMIN.ToString())
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (authCookie["Role"] == Role.SELLER.ToString())
                {
                    return RedirectToAction("Index", "Seller");
                }
                else
                {
                    return RedirectToAction("Index", "Customer");
                }
            }
            List<Product> products = (List<Product>)HttpContext.Application["Products"];

            return View();
        }
    }
}