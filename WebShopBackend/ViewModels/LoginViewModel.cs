using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShopBackend.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required!"), MinLength(2), MaxLength(10)]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required!"), MinLength(2), MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}