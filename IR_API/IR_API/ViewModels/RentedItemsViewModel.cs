using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IR_API.ViewModels
{
    public class RentedItemsViewModel
    {
        public int RentalId { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public DateTime DateRented { get; set;}
        public DateTime? DateReturned { get; set; }
    }
}