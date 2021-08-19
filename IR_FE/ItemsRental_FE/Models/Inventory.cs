using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Models
{
    public class Inventory
    {
        public int ToolId { get; set; }
        public int Assetnumber { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public string ActiveRetired { get; set; }
        public string CheckedOut { get; set; }
        public string Comment { get; set; }
        public string image { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
       
    }

    
}