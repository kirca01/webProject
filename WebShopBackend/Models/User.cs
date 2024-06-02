using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace WebShopBackend.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public Role Role { get; set; }
        public List<Guid> Orders { get; set; }
        public List<Guid> FavoriteProducts { get; set; }
        public List<Guid> PublishedProducts { get; set; }
        public bool IsDeleted { get; set; }

        public User()
        {
            Orders = new List<Guid>();
            FavoriteProducts = new List<Guid>();
            PublishedProducts = new List<Guid>();
        }

        public User(string username, string password, string name, string surname, Gender gender, string email, DateTime birthdate, Role role, bool isDeleted)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Gender = gender;
            Email = email;
            Birthdate = birthdate;
            Role = role;
            Orders = new List<Guid>();
            FavoriteProducts = new List<Guid>();
            PublishedProducts = new List<Guid>();
            IsDeleted = isDeleted;
        }

        public User(string username, string password, string name, string surname, Gender gender, string email, DateTime birthdate, Role role, bool isDeleted, List<Guid> orders, List<Guid> favoriteProducts, List<Guid> publishedProducts) : this(username, password, name, surname, gender, email, birthdate, role, isDeleted)
        {
            Orders = orders;
            FavoriteProducts = favoriteProducts;
            PublishedProducts = publishedProducts;
        }

        public string ToCsvLine()
        {
            StringBuilder csvLine = new StringBuilder();

            csvLine.Append($"{Username};");
            csvLine.Append($"{Password};");
            csvLine.Append($"{Name};");
            csvLine.Append($"{Surname};");
            csvLine.Append($"{Gender};");
            csvLine.Append($"{Email};");
            csvLine.Append($"{Birthdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)};");
            csvLine.Append($"{Role};");
            csvLine.Append($"{IsDeleted};");
            csvLine.Append($"{string.Join(",", Orders)};");
            csvLine.Append($"{string.Join(",", FavoriteProducts)};");
            csvLine.Append($"{string.Join(",", PublishedProducts)}");

            return csvLine.ToString();
        }
    }
}