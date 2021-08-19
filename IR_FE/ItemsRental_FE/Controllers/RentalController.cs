using ItemsRental_FE.Models;
using ItemsRental_FE.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Controllers
{
    public class RentalController : Controller
    {
        // GET: Rental
        public ActionResult Index()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Rentals").Result;
            IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;

            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<List<Customer>>().Result;

            response = WebClient.ApiClient.GetAsync("Workspaces").Result;
            IList<Workspace> workspaces = response.Content.ReadAsAsync<List<Workspace>>().Result;

            var rentalViewModel = rentals.Select(r => new RentalViewModel
            {
                RentalId = r.RentalId,
                workspace = workspaces.Where(w => w.WorkspaceId == r.WorkspaceId).Select(u => u.Workspace1).FirstOrDefault(),
                CustomerName = customers.Where(c => c.CustomerId == r.CustomerId).Select(a => a.CustomerName).FirstOrDefault(),
                DateRented = r.DateRented,
                DateReturned = r.DateReturned
            }).OrderByDescending(o => o.DateRented).ToList();
            return View(rentalViewModel);
        }

        // GET: Rental/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{id}").Result;
            var rental = response.Content.ReadAsAsync<Rental>().Result;

            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

            response = WebClient.ApiClient.GetAsync("Workspaces").Result;
            IList<Workspace> workspace = response.Content.ReadAsAsync<IList<Workspace>>().Result;

            response = WebClient.ApiClient.GetAsync("Inventories").Result;
            IList<Inventory> inventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;

            var rentalViewModel = new RentalViewModel();
            rentalViewModel.RentalId = rental.RentalId;
            rentalViewModel.DateRented = rental.DateRented;
            rentalViewModel.DateReturned = rental.DateReturned;
            rentalViewModel.CustomerName = customers.Where(c => c.CustomerId == rental.CustomerId).Select(cu => cu.CustomerName).FirstOrDefault();
            rentalViewModel.workspace = workspace.Where(w => w.WorkspaceId == rental.WorkspaceId).Select(ws => ws.Workspace1).FirstOrDefault();

            response = WebClient.ApiClient.GetAsync($"RentalItemsById/{id}").Result;
            IList<RentalList> rentalList = response.Content.ReadAsAsync<IList<RentalList>>().Result;

            var rentedItems = rentalList.Select(t => new CustomerItemsViewModel
            {
                RentalListId = t.RentalListId,
                RentalId = t.RentalId,
                Tool = inventories.Where(iv => iv.ToolId == t.ToolId).Select(r => r.Description).FirstOrDefault(),
            }).ToList();

            rentalViewModel.RentedItems = rentedItems;

            return View(rentalViewModel);
        }

        // GET: Rental/Create
        public ActionResult Create()
        {
            var rental = new Rental();
            rental.RentalId = -1;
            rental.DateRented = DateTime.Now;
            rental.Customers = GetCustomers();
            rental.Workspaces = GetWorkspaces();
            return View(rental);
        }

        // POST: Rental/Create
        [HttpPost]
        public ActionResult Create(Rental rental)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Rentals", rental).Result;
                rental = response.Content.ReadAsAsync<Rental>().Result;

                response = WebClient.ApiClient.GetAsync($"RentalItemsById/{rental.RentalId}").Result;
                TempData["SuccessMessage"] = "Action was Success";

                IList<RentalList>  rentalItems = response.Content.ReadAsAsync<List<RentalList>>().Result;
                if (rentalItems.Count == 0)
                    return RedirectToAction("Edit", new { Id = rental.RentalId });
                else
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rental/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{id}").Result;
            var rental = response.Content.ReadAsAsync<Rental>().Result;
            var customers = GetCustomers();
            var workspaces = GetWorkspaces();

            rental.Customers = customers;
            rental.Workspaces = workspaces;

            response = WebClient.ApiClient.GetAsync($"RentalItemsById/{id}").Result;
            IList<RentalList> rentalList = response.Content.ReadAsAsync<IList<RentalList>>().Result;

            response = WebClient.ApiClient.GetAsync("Inventories").Result;
            IList<Inventory> inventory = response.Content.ReadAsAsync<IList<Inventory>>().Result;

            var rentedItems = rentalList.Select(t => new CustomerItemsViewModel
            {
                RentalListId = t.RentalListId,
                RentalId = t.RentalId,
                Tool = inventory.Where(iv => iv.ToolId == t.ToolId).Select(r=>r.Description).FirstOrDefault(),
            }).ToList();

            rental.RentedItems = rentedItems;

            return View(rental);
        }

        // POST: Rental/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Rental rental)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Rentals/{id}",rental).Result;
                TempData["SuccessMessage"] = "Action was Success";
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rental/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{id}").Result;
            var rental = response.Content.ReadAsAsync<Rental>().Result;

            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

            response = WebClient.ApiClient.GetAsync("Workspaces").Result;
            IList<Workspace> workspace = response.Content.ReadAsAsync<IList<Workspace>>().Result;

            response = WebClient.ApiClient.GetAsync("Inventories").Result;
            IList<Inventory> inventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;

            var rentalViewModel = new RentalViewModel();
            rentalViewModel.RentalId = rental.RentalId;
            rentalViewModel.DateRented = rental.DateRented;
            rentalViewModel.DateReturned = rental.DateReturned;
            rentalViewModel.CustomerName = customers.Where(c => c.CustomerId == rental.CustomerId).Select(cu => cu.CustomerName).FirstOrDefault();
            rentalViewModel.workspace = workspace.Where(w => w.WorkspaceId == rental.WorkspaceId).Select(ws => ws.Workspace1).FirstOrDefault();

            response = WebClient.ApiClient.GetAsync($"RentalItemsById/{id}").Result;
            IList<RentalList> rentalList = response.Content.ReadAsAsync<IList<RentalList>>().Result;

            var rentedItems = rentalList.Select(t => new CustomerItemsViewModel
            {
                RentalListId = t.RentalListId,
                RentalId = t.RentalId,
                Tool = inventories.Where(iv => iv.ToolId == t.ToolId).Select(r => r.Description).FirstOrDefault(),
            }).ToList();

            rentalViewModel.RentedItems = rentedItems;

            return View(rentalViewModel);

        }

        // POST: Rental/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,Rental rental)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Rentals/{id}").Result;
                TempData["SuccessMessage"] = "Action was Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // AddItems - GET
        public ActionResult AddTools(int RentalId)
        {
            var rentalList = new RentalList();
            var tool = GetTools();
            rentalList.RentalId = RentalId;
            rentalList.Tool = tool;

            return View(rentalList);
        }

        // AddItems - POST
        [HttpPost]
        public ActionResult AddTools(RentalList rentalList)
        {
            try
            {
                int id = rentalList.RentalId;
                HttpResponseMessage respone = WebClient.ApiClient.PostAsJsonAsync("RentalLists", rentalList).Result;
                TempData["SuccessMessage"] = "Action was Success";
                return RedirectToAction("Edit", new { id });
            }
            catch
            {

                return View("No record of the associated rental can be found.\n" +
                            "Make sure to submit the Rental details before add the movies");
            }

        }

        // EditRentedItem - GET
        public ActionResult EditRentedItem(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalLists/{id}").Result;
            var rentalList = response.Content.ReadAsAsync<RentalList>().Result;
            var tool = GetTools();

            rentalList.Tool = tool;

            return View(rentalList);
        }

        // EditRentedItem - POST
        [HttpPost]
        public ActionResult EditRentedItem(int id,RentalList rentalList)
        {
            try
            {
                int rentalId = rentalList.RentalId;
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"RentalLists/{id}", rentalList).Result;
                TempData["SuccessMessage"] = "Action was Success";
                if (response.IsSuccessStatusCode)
                    return RedirectToAction($"Edit/{rentalId}");
                return RedirectToAction($"Edit/{rentalId}");
            }
            catch
            {
                return View();
            }

        }

        // DeleteRentedItem - Get
        public ActionResult DeleteRentedItem(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalLists/{id}").Result;
            var rentalList = response.Content.ReadAsAsync<RentalList>().Result;

            response = WebClient.ApiClient.GetAsync("Inventories").Result;
            var tool = response.Content.ReadAsAsync<IList<Inventory>>().Result;

            rentalList.ToolName = tool.Where(o => o.ToolId == rentalList.ToolId).Select(t => t.Description).FirstOrDefault();
            rentalList.RentalListId = id;

            return View(rentalList);
        }

        // DeleteRentedItem - Post
        [HttpPost]
        public ActionResult DeleteRentedItem(int id,RentalList rentalList)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalLists/{id}").Result;
                TempData["SuccessMessage"] = "Action was Success";
                var rl = response.Content.ReadAsAsync<RentalList>().Result;
                int rentalId = rl.RentalId;

                response = WebClient.ApiClient.DeleteAsync($"RentalLists/{id}").Result;
                return RedirectToAction($"Edit/{rentalId}");
            }
            catch
            {
                return View();
                throw;
            }
        }


        #region Helper Methods
        /// <summary>
        /// /This will help to populate the dropdown box
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetBrand()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Brands").Result;
            IList<Brand> brands = response.Content.ReadAsAsync<IList<Brand>>().Result;
            List<SelectListItem> brandslist = brands.OrderBy(o => o.Brand1).Select(m => new SelectListItem
            {
                Value = m.BrandId.ToString(),
                Text = m.Brand1,
            }).ToList();

            return new SelectList(brandslist, "Value", "Text");
        }
        /// <summary>
        /// This method will return the list of tools
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("inventories").Result;
            IList<Inventory> inventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;
            List<SelectListItem> inventorieslist = inventories.OrderBy(i => i.Assetnumber).Select(n => new SelectListItem
            {
                Value = n.ToolId.ToString(),
                Text = n.Description,
            }).ToList();

            return new SelectList(inventorieslist, "Value", "Text");
        }

        /// <summary>
        /// /This will help to populate the dropdown box
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetCustomers()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

            List<SelectListItem> customerlist = customers.OrderBy(o => o.CustomerName).Select(c => new SelectListItem
            {
                Value = c.CustomerId.ToString(),
                Text = c.CustomerName

            }).ToList();

            return new SelectList(customerlist, "Value", "Text");
        }
        /// <summary>
        /// This method will return the list of workspaces
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetWorkspaces()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Workspaces").Result;
            IList<Workspace> workspaces = response.Content.ReadAsAsync<IList<Workspace>>().Result;

            List<SelectListItem> workspacelist = workspaces.OrderBy(ws => ws.Workspace1).Select(w => new SelectListItem
            {
                Value = w.WorkspaceId.ToString(),
                Text = w.Workspace1,
            }).ToList();

            return new SelectList(workspacelist, "Value", "Text");
        }

        #endregion
    }
}
