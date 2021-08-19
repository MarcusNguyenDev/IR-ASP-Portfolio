using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.ViewModel
{
    public class InventoryViewModel
    {
        public int ToolId { get; set; }
        [Display(Name = "Asset number")]
        public int Assetnumber { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        [Display(Name = "Brand Name")]
        public string Brandname { get; set; }
        [Display(Name = "Active or Retired")]
        public string ActiveRetired { get; set; }
        [Display(Name = "Checked Out?")]
        public string CheckedOut { get; set; }
        public string Comment { get; set; }
        public string image { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}