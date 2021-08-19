using ItemsRental_FE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItemsRental_FE.ViewModel
{
    public class CustomerRentalsDetailViewModel
    {
        public Rental Rental { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        public List<CustomerItemsViewModel> RentedItems { get; set; }
    }
}