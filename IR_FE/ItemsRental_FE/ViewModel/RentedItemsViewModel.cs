using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItemsRental_FE.ViewModel
{
    public class RentedItemsViewModel
    {
        public int RentalId { get; set; }
        public string CustomerName { get; set; }
        public string Tool { get; set; }
        public string Description { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}