using IR_API.Models;
using IR_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IR_API.Controllers
{
    public class ReportsController : ApiController
    {
        private ItemRentalEntities db = new ItemRentalEntities();
        [HttpGet]
        [Route("api/Reports/GetRentedItemsReport")]
        public IEnumerable<RentedItemsViewModel> GetRentedItemsReport()
        {
            string sql = "SELECT Rental.RentalId, Customer.CustomerName, Inventory.Description, Rental.DateRented, Rental.DateReturned " +
                "FROM Customer INNER JOIN Rental ON Customer.CustomerId = Rental.CustomerId " +
                "INNER JOIN RentalList ON Rental.RentalId = RentalList.RentalId " +
                "INNER JOIN Inventory ON RentalList.ToolId = Inventory.ToolId";
            return db.Database.SqlQuery<RentedItemsViewModel>(sql).ToList();
        }

        [HttpGet]
        [Route("api/Reports/GetItemRentalCountReport")]
        public IEnumerable<ItemRentalCountReport> GetItemRentalCountReport()
        {
            return db.ItemRentalCountReports;
        }
    }
}
