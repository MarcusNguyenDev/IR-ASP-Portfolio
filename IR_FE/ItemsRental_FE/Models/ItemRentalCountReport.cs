using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItemsRental_FE.Models
{
    public class ItemRentalCountReport
    {
        public int ToolId { get; set; }
        public string Description { get; set; }
        //public string ToolName { get; set; }
        public int? RentalCount { get; set; }
    }
}