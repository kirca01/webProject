using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBackend.Models;

namespace WebShopBackend.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeOrder()
        {
            Guid id = Guid.Parse(Request.QueryString["productId"]);
            int amount = int.Parse(Request.QueryString["amount"]);
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            Product product = products.FirstOrDefault(p => p.Id == id);
            if (product.Amount < amount)
                throw new HttpException(400, "Bad Request");

            product.Amount -= amount;
            Order order = new Order(Guid.NewGuid(), id, amount, username, DateTime.Today, Status.ACTIVE);
            Data.SaveOrders("~/App_Data/orders.txt", order);
            List<Order> orders = (List<Order>)HttpContext.Application["Orders"];
            orders.Add(order);
            HttpContext.Application["Orders"] = orders;
            Data.SaveProduct("~/App_Data/products.txt", product);


            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOrdersForCustomer()
        {
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            List<Order> orders = (List<Order>)HttpContext.Application["Orders"];
            List<Order> userOrders = orders.FindAll(o => o.User.Equals(username));
            return Json(userOrders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetOrderStatusToDone()
        {
            Guid id = Guid.Parse(Request.QueryString["orderId"]);
            List<Order> orders = (List<Order>)HttpContext.Application["Orders"];
            Order order = orders.FirstOrDefault(o => o.Id == id);
            order.Status = Status.DONE;
            Data.SaveOrders("~/App_Data/orders.txt", order);
            return Json(order, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostReview()
        {
            Guid id = Guid.Parse(Request.QueryString["orderId"]);
            List<Order> orders = (List<Order>)HttpContext.Application["Orders"];
            Order order = orders.FirstOrDefault(o => o.Id == id);
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            string title = Request.QueryString["title"];
            string comment = Request.QueryString["comment"];
            List<Product> products = (List<Product>)HttpContext.Application["Products"];
            Product product = products.FirstOrDefault(p => p.Id == order.Product);
            Review review = new Review(Guid.NewGuid(), order.Product, username, title, comment, product.Image, false);
            Data.SaveReviews("~/App_Data/reviews.txt", review);
            List<Review> reviews = (List<Review>)HttpContext.Application["Reviews"];
            reviews.Add(review);
            HttpContext.Application["Reviews"] = reviews;

            return Json(review, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetReviewsForProduct()
        {
            Guid id = Guid.Parse(Request.QueryString["productId"]);
            HttpCookie cookie = Request.Cookies["AuthToken"];
            string username = cookie["Username"];
            List<Review> reviews = (List<Review>)HttpContext.Application["Reviews"];
            List<Review> reviewsForProduct = reviews.FindAll(rew => rew.User.Equals(username) && rew.Product == id && rew.IsDeleted == false);
            return Json(reviewsForProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteReview()
        {
            Guid id = Guid.Parse(Request.QueryString["reviewId"]);
            List<Review> reviews = (List<Review>)HttpContext.Application["Reviews"];
            Review review = reviews.FirstOrDefault(o => o.Id == id);
            review.IsDeleted = true;
            Data.SaveReviews("~/App_Data/reviews.txt", review);
            return Json(review, JsonRequestBehavior.AllowGet);
        }
    }
}