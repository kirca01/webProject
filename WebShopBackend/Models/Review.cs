using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebShopBackend.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid Product { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

        public Review()
        {

        }
        public Review(Guid id, Guid product, string user, string title, string description, string image, bool isDeleted)
        {
            Id = id;
            Product = product;
            User = user;
            Title = title;
            Description = description;
            Image = image;
            IsDeleted = isDeleted;
        }

        public string ToCsvLine()
        {
            StringBuilder csvLine = new StringBuilder();

            csvLine.Append($"{Id};");
            csvLine.Append($"{Product};");
            csvLine.Append($"{User};");
            csvLine.Append($"{Title};");
            csvLine.Append($"{Description};");
            csvLine.Append($"{Image};");
            csvLine.Append($"{IsDeleted}");

            return csvLine.ToString();
        }
    }
}