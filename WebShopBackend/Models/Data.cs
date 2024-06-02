using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebShopBackend.Models
{
    public class Data
    {
        public static List<User> ReadUsers(string path)
        {
            List<User> users = new List<User>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                string[] orders = tokens[9].Split(',');
                string[] favoriteProducts = tokens[10].Split(',');
                string[] publishedProducts = tokens[11].Split(',');

                List<Guid> ordersList = new List<Guid>();
                if(!orders[0].Equals(""))
                    foreach (string order in orders)
                    {
                        ordersList.Add(Guid.Parse(order));
                    }

                List<Guid> favoriteList = new List<Guid>();
                if (!favoriteProducts[0].Equals(""))
                    foreach (string favorite in favoriteProducts)
                    {
                        favoriteList.Add(Guid.Parse(favorite));
                    }

                List<Guid> publishedList = new List<Guid>();
                if (!publishedProducts[0].Equals(""))
                    foreach (string published in publishedProducts)
                    {
                        publishedList.Add(Guid.Parse(published));
                    }

                User user = new User(tokens[0], tokens[1], tokens[2], tokens[3], (Gender)Enum.Parse(typeof(Gender), tokens[4]), tokens[5], DateTime.Parse(tokens[6]), (Role)Enum.Parse(typeof(Role), tokens[7]), bool.Parse(tokens[8]), ordersList, favoriteList, publishedList);
                users.Add(user);
            }
            sr.Close();
            stream.Close();

            return users;
        }

        public static void SaveUser(string path, User user)
        { 
            string newUser = user.ToCsvLine();
            path = HostingEnvironment.MapPath(path);         
            string[] lines = File.ReadAllLines(path);
            bool userExists = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(';');
                if(tokens.Length > 0 && tokens[0].Equals(user.Username))
                {
                    lines[i] = newUser;
                    userExists = true;
                    break;
                }
            }
            if (!userExists)
            {
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = newUser;
            }
            File.WriteAllLines(path, lines);     
        }
        public static List<Product> ReadProducts(string path)
        {
            List<Product> products = new List<Product>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                string[] listOfReviews = tokens[11].Split(','); 
                List<Guid> reviewList = new List<Guid>();
                if (!listOfReviews[0].Equals(""))
                    foreach (string review in listOfReviews)
                    {
                        reviewList.Add(Guid.Parse(review));
                    }

                Product product = new Product(Guid.Parse(tokens[0]), tokens[1], tokens[2], double.Parse(tokens[3]), int.Parse(tokens[4]), tokens[5], tokens[6], DateTime.Parse(tokens[7]), tokens[8], bool.Parse(tokens[9]), bool.Parse(tokens[10]), reviewList);
                products.Add(product);
            }
 
            sr.Close();
            stream.Close();

            return products;
        }

        public static void SaveProduct(string path, Product product)
        {
            string newProduct = product.ToCsvLine();
            path = HostingEnvironment.MapPath(path);
            string[] lines = File.ReadAllLines(path);
            bool productExists = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(';');
                if (tokens.Length > 0 && tokens[0].Equals(product.Id.ToString()))
                {
                    lines[i] = newProduct;
                    productExists = true;
                    break;
                }
            }
            if (!productExists)
            {
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = newProduct;
            }
            File.WriteAllLines(path, lines);
        }
        public static List<Order> ReadOrders(string path)
        {
            List<Order> orders = new List<Order>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Order order = new Order(Guid.Parse(tokens[0]), Guid.Parse(tokens[1]), int.Parse(tokens[2]), tokens[3], DateTime.Parse(tokens[4]), (Status)Enum.Parse(typeof(Status), tokens[5]));
                orders.Add(order);
            }
            sr.Close();
            stream.Close();

            return orders;
        }

        public static void SaveOrders(string path, Order order)
        {
            string newOrder = order.ToCsvLine();
            path = HostingEnvironment.MapPath(path);
            string[] lines = File.ReadAllLines(path);
            bool orderExists = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(';');
                if (tokens.Length > 0 && tokens[0].Equals(order.Id.ToString()))
                {
                    lines[i] = newOrder;
                    orderExists = true;
                    break;
                }
            }
            if (!orderExists)
            {
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = newOrder;
            }
            File.WriteAllLines(path, lines);
        }

        public static List<Review> ReadReviews(string path)
        {
            List<Review> reviews = new List<Review>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Review review = new Review(Guid.Parse(tokens[0]), Guid.Parse(tokens[1]), tokens[2], tokens[3], tokens[4], tokens[5], bool.Parse(tokens[6]));
                reviews.Add(review);
            }
            sr.Close();
            stream.Close();

            return reviews;
        }

        public static void SaveReviews(string path, Review review)
        {
            string newReview = review.ToCsvLine();
            path = HostingEnvironment.MapPath(path);
            string[] lines = File.ReadAllLines(path);
            bool reviewExists = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(';');
                if (tokens.Length > 0 && tokens[0].Equals(review.Id.ToString()))
                {
                    lines[i] = newReview;
                    reviewExists = true;
                    break;
                }
            }
            if (!reviewExists)
            {
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = newReview;
            }
            File.WriteAllLines(path, lines);
        }
    }
}