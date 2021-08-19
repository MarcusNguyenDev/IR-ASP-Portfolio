using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Models
{
    public class RentalList
    {
        public int RentalListId { get; set; }
        public int RentalId { get; set; }
        public int ToolId { get; set; }
        public IEnumerable<SelectListItem> Tool { get; set; }
        public string ToolName { get; set; }
    }
    
}