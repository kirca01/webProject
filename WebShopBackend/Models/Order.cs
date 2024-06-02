using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace WebShopBackend.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid Product { get; set; }
        public int Amount { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }

        public Order()
        {

        }
        public Order(Guid id, Guid product, int amount, string user, DateTime date, Status status)
        {
            Id = id;
            Product = product;
            Amount = amount;
            User = user;
            Date = date;
            Status = status;
        }

        public string ToCsvLine()
        {
            StringBuilder csvLine = new StringBuilder();

            csvLine.Append($"{Id};");
            csvLine.Append($"{Product};");
            csvLine.Append($"{Amount};");
            csvLine.Append($"{User};");
            csvLine.Append($"{Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)};");
            csvLine.Append($"{Status}");

            return csvLine.ToString();
        }
    }
}