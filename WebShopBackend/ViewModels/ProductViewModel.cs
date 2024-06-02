using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShopBackend.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id is required!")]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required!"), MinLength(2), MaxLength(10)]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required!")]
        public double Price { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Amount is required!")]
        public int Amount { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required!")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }
    }
}