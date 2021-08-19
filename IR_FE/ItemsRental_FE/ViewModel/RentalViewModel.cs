using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItemsRental_FE.ViewModel
{
    public class RentalViewModel
    {
        public int RentalId { get; set; }
        [Display(Name = "Workspace")]
        public string workspace { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Date Rented")]
        public DateTime DateRented {get;set;}
        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }
        public IEnumerable<CustomerItemsViewModel> RentedItems { get; set; }
    }
}