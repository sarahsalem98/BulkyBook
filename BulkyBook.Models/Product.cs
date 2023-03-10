using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        public int Id { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]

        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        public string Author { get; set; }
        [Required]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "price for 1-50")]
        public double Price { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "price for 51-100")]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }


        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        [Display(Name ="Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType {get;set;}

    }
}
