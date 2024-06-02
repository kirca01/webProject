using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBackend.Models;

namespace WebShopBackend.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult getLoggedUser()
        {
            HttpCookie cookie = Request.Cookies["AuthToken"]; 
            string username = cookie["Username"];
            List<User> users = (List<User>)HttpContext.Application["Users"];
            User user = users.FirstOrDefault(u => u.Username == username);

            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}