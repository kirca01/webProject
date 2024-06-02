using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebShopBackend.Models;

namespace WebShopBackend.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required!"), MinLength(2), MaxLength(10)]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required!"), MinLength(2), MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required!")]
        public Gender Gender { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Birthdate")]
        [Required(ErrorMessage = "Birthdate is required!")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}