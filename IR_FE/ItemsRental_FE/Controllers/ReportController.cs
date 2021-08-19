using ItemsRental_FE.Models;
using ItemsRental_FE.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Controllers
{
    public class ReportController : Controller
    {
        //GET: Report
        public ActionResult GetRentalCountData()
        {
            IEnumerable<ItemRentalCountReport> itemRentalCountReport = GetItemsRentalCountReport();
            return View(itemRentalCountReport.OrderBy(m => m.Description));
        }

        public IEnumerable<ItemRentalCountReport> GetItemsRentalCountReport()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetItemRentalCountReport").Result;
            IEnumerable<ItemRentalCountReport> report = response.Content.ReadAsAsync<IEnumerable<ItemRentalCountReport>>().Result;
            //var itemRentalCountReport = report.Select(r => new ItemRentalCountRepor
            //{
            //    ToolId = r.ToolId,
            //    Description = r.Description,
            //    //ToolName = r.Description,
            //    RentalCount = r.RentalCount
            //});

            return report;
        }

        public ActionResult DrawItemRentalCountChart()
        {
            IEnumerable<ItemRentalCountReport> itemRentalCountReport = GetItemsRentalCountReport();
            return View(itemRentalCountReport);
        }


        [HttpGet]
        public ActionResult GetRentedItemsReport(string criteria)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetRentedItemsReport").Result;
            IEnumerable<RentedItemsViewModel> rentedItemReport = response.Content.ReadAsAsync<IEnumerable<RentedItemsViewModel>>().Result;

            var RIreport = rentedItemReport.Select(r => new RentedItemsViewModel
            {
                RentalId=r.RentalId,
                Tool=r.Description,
                CustomerName=r.CustomerName,
                Description=r.Description,
                DateRented=r.DateRented,
                DateReturned=r.DateReturned
            });

            
            if (string.IsNullOrEmpty(criteria))
            {
                TempData["RentedItems"] = RIreport;
                return View(RIreport);
            }

            else
            {
                RIreport = RIreport.Where(m => m.Tool.ToLower().Contains(criteria.ToLower()) || m.CustomerName.ToLower().Contains(criteria.ToLower())).ToList();
                return View(RIreport);
            }
        }

        public void ExportRentedItemData()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetRentedItemsReport").Result;
            IEnumerable<RentedItemsViewModel> rentedItemReport = response.Content.ReadAsAsync<IEnumerable<RentedItemsViewModel>>().Result;

            var RIreport = rentedItemReport.Select(r => new RentedItemsViewModel
            {
                RentalId = r.RentalId,
                Tool = r.Description,
                CustomerName = r.CustomerName,
                Description = r.Description,
                DateRented = r.DateRented,
                DateReturned = r.DateReturned
            });

            //List<RentedItemsViewModel> rentedItems = RIreport as List<RentedItemsViewModel>;

            StringWriter sw = new StringWriter();
            sw.WriteLine("\"RentalId\",\"Tool\",\"CustomerName\",\"DateRented\",\"DateReturned\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=RentedItems.csv");
            Response.ContentType = "application/octet-stream";
            foreach (var rentedItem in RIreport)
            {
                sw.WriteLine($"{rentedItem.RentalId},{rentedItem.Tool},{rentedItem.CustomerName},{rentedItem.DateRented},{rentedItem.DateReturned}");
            }
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
