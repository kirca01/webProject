using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBackend.Models;
using WebShopBackend.ViewModels;

namespace WebShopBackend.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        // GET: Auth/Login
        public ActionResult Login()
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
            if (Session["loggedUser"] != null) return RedirectToAction("Index", "User");
            return View();
        }

        // GET: Auth/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<User> users = (List<User>)HttpContext.Application["Users"];
                bool usernameExists = users.Any(user => user.Username == model.Username);
                if (usernameExists) return View();
                User newUser = new User(model.Username, model.Password, model.Name, model.Surname, model.Gender, model.Email, model.Birthdate, Role.CUSTOMER, false);
                Data.SaveUser("~/App_Data/users.txt", newUser);
                users.Add(newUser);
                HttpContext.Application["Users"] = users;
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<User> users = (List<User>)HttpContext.Application["Users"];
                User foundUser = users.FirstOrDefault(user => user.Username == model.Username && user.Password == model.Password);

                if (foundUser == null)
                {
                    return View();
                }
                HttpCookie authCookie = new HttpCookie("AuthToken");
                authCookie["Username"] = model.Username;
                authCookie["Role"] = foundUser.Role.ToString();
                authCookie.Expires = DateTime.Now.AddMonths(1);

                Response.Cookies.Add(authCookie);
                if (foundUser.Role == Role.CUSTOMER)
                    return RedirectToAction("Index", "Customer");
                else if(foundUser.Role == Role.SELLER)
                    return RedirectToAction("Index", "Seller");
                else
                    return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        // POST: Auth/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            HttpCookie authCookie = new HttpCookie("AuthToken");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult GetLoggedUser()
        {
            HttpCookie cookie = Request.Cookies["AuthToken"]; 
            string username = cookie["Username"];
            List<User> users = (List<User>)HttpContext.Application["Users"];
            User user = users.FirstOrDefault(u => u.Username == username);

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProfile(RegisterViewModel model)
        {
            List<User> users = (List<User>)HttpContext.Application["Users"];
            bool usernameExists = users.Any(user => user.Username == model.Username);
            if (!usernameExists) return View();
            User oldUser = users.FirstOrDefault(u => u.Username == model.Username);
            oldUser.Password = model.Password;
            oldUser.Name = model.Name;
            oldUser.Surname = model.Surname;
            oldUser.Email = model.Email;
            Data.SaveUser("~/App_Data/users.txt", oldUser);

            if (oldUser.Role == Role.ADMIN)
            {
                return RedirectToAction("Profile", "Admin");
            }
            else if (oldUser.Role == Role.SELLER)
            {
                return RedirectToAction("Profile", "Seller");
            }
            else
            {
                return RedirectToAction("Profile", "Customer");
            }
        }
    }
}