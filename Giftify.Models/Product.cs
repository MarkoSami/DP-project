using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        [Display(Name ="Product Name")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Range(1,20000)]
        public int Price { get; set; }

        [Display(Name = "Price - Quantity 10 or More")]

        [Range(1, 20000)]
        public int Price10 { get; set; }

        [Display(Name = "Price - Quantity 20 or More")]
        [Range(1, 20000)]
        public int Price20 { get; set; }
        [ValidateNever]
        public string ImageURL{ get; set; }
        public int CategoryId{ get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
