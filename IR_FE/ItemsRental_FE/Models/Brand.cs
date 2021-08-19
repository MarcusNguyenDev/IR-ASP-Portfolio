using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItemsRental_FE.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Display(Name = "Brand Name")]
        public string Brand1 { get; set; }
    }
}