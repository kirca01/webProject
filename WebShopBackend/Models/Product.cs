using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace WebShopBackend.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string SellerUsername { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime UploadDate { get; set; }
        public string City { get; set; }
        public bool IsAvailable { get; set; }
        public List<Guid> Reviews { get; set; }
        public bool IsDeleted { get; set; }

        public Product()
        {
            Reviews = new List<Guid>();
        }

        public Product(Guid id, string sellerUsername, string name, double price, int amount, string description, string image, DateTime uploadDate, string city, bool isAvailable, bool isDeleted)
        {
            Id = id;
            SellerUsername = sellerUsername;
            Name = name;
            Price = price;
            Amount = amount;
            Description = description;
            Image = image;
            UploadDate = uploadDate;
            City = city;
            IsAvailable = isAvailable;
            Reviews = new List<Guid>();
            IsDeleted = isDeleted;
        }

        public Product(Guid id, string sellerUsername, string name, double price, int amount, string description, string image, DateTime uploadDate, string city, bool isAvailable, bool isDeleted, List<Guid> reviews) : this(id, sellerUsername, name, price, amount, description, image, uploadDate, city, isAvailable, isDeleted)
        {
            Reviews = reviews;
        }

        public string ToCsvLine()
        {
            StringBuilder csvLine = new StringBuilder();

            csvLine.Append($"{Id};");
            csvLine.Append($"{SellerUsername};");
            csvLine.Append($"{Name};");
            csvLine.Append($"{Price};");
            csvLine.Append($"{Amount};");
            csvLine.Append($"{Description};");
            csvLine.Append($"{Image};");
            csvLine.Append($"{UploadDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)};");
            csvLine.Append($"{City};");
            csvLine.Append($"{IsAvailable};");
            csvLine.Append($"{IsDeleted};");
            csvLine.Append($"{string.Join(",", Reviews)}");

            return csvLine.ToString();
        }
    }
}