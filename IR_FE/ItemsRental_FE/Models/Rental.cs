using ItemsRental_FE.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public int WorkspaceId { get; set; }
        [Display(Name = "Date Rented")]
        public DateTime DateRented { get; set; }
        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }
        public virtual ICollection<RentalList> RentalLists { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }
        public IEnumerable<SelectListItem> Workspaces { get; set; }
        public IEnumerable<CustomerItemsViewModel> RentedItems { get; set; }
    }
}