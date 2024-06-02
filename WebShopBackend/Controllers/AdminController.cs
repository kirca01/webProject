using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBackend.Models;
using WebShopBackend.ViewModels;

namespace WebShopBackend.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SellerViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<User> users = (List<User>)HttpContext.Application["Users"];
                bool usernameExists = users.Any(user => user.Username == model.Username);
                if (!usernameExists)
                {
                    User newUser = new User(model.Username, model.Password, model.Name, model.Surname, model.Gender, model.Email, model.Birthdate, Role.SELLER, false);
                    Data.SaveUser("~/App_Data/users.txt", newUser);
                    users.Add(newUser);
                    HttpContext.Application["Users"] = users;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    User user = users.FirstOrDefault(u => u.Username == model.Username);
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Email = model.Email;
                    user.Birthdate = model.Birthdate;
                    Data.SaveUser("~/App_Data/users.txt", user);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            List<User> users = (List<User>)HttpContext.Application["Users"];
            List<User> newUsers = users.FindAll(p => p.IsDeleted == false);
            return Json(newUsers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFilteredUsers()
        {
            List<User> users = (List<User>)HttpContext.Application["Users"];
            string name = Request.QueryString["name"];
            if (name == null) name = "";
            string surname = Request.QueryString["surname"];
            if (surname == null) surname = "";
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            Role? role = null;

            if(DateTime.TryParse(Request.QueryString["dateFrom"], out DateTime parsedDateFrom))
            {
                dateFrom = parsedDateFrom;
            }

            if(DateTime.TryParse(Request.QueryString["dateTo"], out DateTime parsedDateTo))
            {
                dateTo = parsedDateTo;
            }

            if(Role.TryParse(Request.QueryString["role"], out Role parsedRole))
            {
                role = parsedRole;
            }

            List<User> filteredUsers = users.Where(user => user.Name.ToLower().Contains(name.ToLower()) && user.Surname.ToLower().Contains(surname.ToLower()) && (dateFrom == null || user.Birthdate >= dateFrom) && (dateTo == null || user.Birthdate <= dateTo) && (role == null || user.Role == role)).ToList();

            return Json(filteredUsers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteUserByUsername()
        {
            string username = Request.QueryString["username"];
            List<User> users = (List<User>)HttpContext.Application["Users"];
            User user = users.FirstOrDefault(u => u.Username == username);
            user.IsDeleted = true;
            Data.SaveUser("~/App_Data/users.txt", user);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

    }
}