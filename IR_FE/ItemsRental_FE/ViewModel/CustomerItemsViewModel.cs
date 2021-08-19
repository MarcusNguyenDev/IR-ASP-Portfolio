using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.ViewModel
{
    public class CustomerItemsViewModel
    {
        public int RentalId { get; set; }
        public int RentalListId { get; set; }
        public string Tool { get; set; }
        public int ToolId { get; set; }
        public SelectListItem ToolName { get; set; }
        public IEnumerable<CustomerItemsViewModel> RentedItems { get; set; }
    }
}