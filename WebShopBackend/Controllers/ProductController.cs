using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBackend.Models;
using WebShopBackend.ViewModels;

namespace WebShopBackend.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            List<Product> products = (List<Product>)HttpContext.Application["Products"];

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductById()
        {
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            Guid id = Guid.Parse(Request.QueryString["productId"]);
            Product product = products.FirstOrDefault(p => p.Id == id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductsBySeller()
        {
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            List<Product> productsBySeller = products.FindAll(p => (p.SellerUsername == username || cookie["Role"] == Role.ADMIN.ToString()) && p.IsDeleted == false);
            return Json(productsBySeller, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFilteredProducts()
        {
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            string name = Request.QueryString["name"];
            if (name == null) name = "";
            string city = Request.QueryString["city"];
            if (city == null) city = "";
            double minPrice = 0;
            if (Request.QueryString["minPrice"].Equals("")) minPrice = -1;
            else minPrice = Convert.ToDouble(Request.QueryString["minPrice"]);
            double maxPrice = 0;
            if (Request.QueryString["maxPrice"].Equals("")) maxPrice = 9999999999;
            else maxPrice = Convert.ToDouble(Request.QueryString["maxPrice"]);
            List<Product> filteredProducts = products.Where(product => product.Name.ToLower().Contains(name.ToLower()) && product.City.ToLower().Contains(city.ToLower()) && product.Price >= minPrice && product.Price <= maxPrice).ToList();


            return Json(filteredProducts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFavouriteProducts()
        {
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            List<User> users = (List<User>)HttpContext.Application["Users"];
            User user = users.FirstOrDefault(u => u.Username == username);
            List<Product> favoriteListProduct = new List<Product>();
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            foreach (Product product in products)
            {
                if (user.FavoriteProducts.Contains(product.Id))
                    favoriteListProduct.Add(product);
            }

            return Json(favoriteListProduct, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetProductsFromCart()
        {
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            List<User> users = (List<User>)HttpContext.Application["Users"];
            User user = users.FirstOrDefault(u => u.Username == username); 
            List<Product> shoppingCart = Session["shoppingCart"] as List<Product>;

            return Json(shoppingCart, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProductById()
        {
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            Guid id = Guid.Parse(Request.QueryString["productId"]);
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            Product product = products.FirstOrDefault(p => p.Id == id && (p.SellerUsername.Equals(username) || cookie["Role"].Equals(Role.ADMIN.ToString())));
            product.IsDeleted = true;
            Data.SaveProduct("~/App_Data/products.txt", product);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if(model.Id == new Guid())
            {
                HttpCookie cookie = Request.Cookies["AuthToken"];
                string username = cookie["Username"];
                byte[] fileData = new byte[model.ImageFile.ContentLength];
                model.ImageFile.InputStream.Read(fileData, 0, model.ImageFile.ContentLength);
                string base64Image = Convert.ToBase64String(fileData);
                Product product = new Product(Guid.NewGuid(), username, model.Name, model.Price, model.Amount, model.Description, base64Image, DateTime.Now, model.City, true, false);
                Data.SaveProduct("~/App_Data/products.txt", product);
                List<Product> products = (List<Product>)HttpContext.Application["Products"];
                products.Add(product);
                HttpContext.Application["Products"] = products;
                return RedirectToAction("Index", "Seller");
            }
            else
            {
                List<Product> products = (List<Product>)HttpContext.Application["Products"];
                Product product = products.FirstOrDefault(p => p.Id == model.Id);
                if(model.ImageFile != null)
                {
                    byte[] fileData = new byte[model.ImageFile.ContentLength];
                    model.ImageFile.InputStream.Read(fileData, 0, model.ImageFile.ContentLength);
                    string base64Image = Convert.ToBase64String(fileData);
                    product.Image = base64Image;
                }
                product.Name = model.Name;
                product.Price = model.Price;
                product.Amount = model.Amount;
                product.Description = model.Description;
                product.City = model.City;
                Data.SaveProduct("~/App_Data/products.txt", product);
                return RedirectToAction("Index", "Seller");
            }

        }
    }
}