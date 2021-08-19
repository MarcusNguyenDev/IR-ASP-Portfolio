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
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Inventories").Result;
            IEnumerable<Inventory> inventories = response.Content.ReadAsAsync<IEnumerable<Inventory>>().Result;

            response = WebClient.ApiClient.GetAsync("Brands").Result;
            IList<Brand> brands = response.Content.ReadAsAsync<List<Brand>>().Result;

            var inventoryViewModel = inventories.Select(r => new InventoryViewModel
            {
                ToolId = r.ToolId,
                Assetnumber = r.Assetnumber,
                Description = r.Description,
                Brandname = brands.Where(c => c.BrandId == r.BrandId).Select(u => u.Brand1).FirstOrDefault(),
                ActiveRetired = r.ActiveRetired,
                CheckedOut = r.CheckedOut,
                image = r.image  ,
                Comment=r.Comment

            }).OrderByDescending(a=>a.ToolId).ToList();
            return View(inventoryViewModel);
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Inventories/{id}").Result;
            var inventories = response.Content.ReadAsAsync<Inventory>().Result;

            response = WebClient.ApiClient.GetAsync("Brands").Result;
            IList<Brand> brands = response.Content.ReadAsAsync<List<Brand>>().Result;
            var inventoryViewModel = new InventoryViewModel
            {
                ToolId = inventories.ToolId,
                Assetnumber = inventories.Assetnumber,
                Description = inventories.Description,
                Brandname = brands.Where(c => c.BrandId == inventories.BrandId).Select(u => u.Brand1).FirstOrDefault(),
                ActiveRetired = inventories.ActiveRetired,
                CheckedOut = inventories.CheckedOut,
                image = inventories.image,
                Comment = inventories.Comment
                
            };
            return View(inventoryViewModel);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            Inventory inventory = new Inventory();
            inventory.ToolId = -999;
            inventory.Brands = GetBrands();
            return View(inventory);
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult Create(Inventory inventory)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Inventories", inventory).Result;
                TempData["SuccessMessage"] = "Action was Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Inventories/{id}").Result;
            var inventories = response.Content.ReadAsAsync<Inventory>().Result;

            response = WebClient.ApiClient.GetAsync("Brands").Result;
            IList<Brand> brands = response.Content.ReadAsAsync<List<Brand>>().Result;
            inventories.Brands = GetBrands();

            var inventoryViewModel = new InventoryViewModel
            {
                ToolId = inventories.ToolId,
                Assetnumber = inventories.Assetnumber,
                Description = inventories.Description,
                BrandId = inventories.BrandId,
                Brandname = brands.Where(c => c.BrandId == inventories.BrandId).Select(u => u.Brand1).FirstOrDefault(),
                ActiveRetired = inventories.ActiveRetired,
                CheckedOut = inventories.CheckedOut,
                Brands = inventories.Brands,
                Comment=inventories.Comment,
                image = inventories.image,
            };
            return View(inventoryViewModel);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Inventory inventory)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Inventories/{id}", inventory).Result;
                TempData["SuccessMessage"] = "Action was Success";

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(inventory);
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Inventories/{id}").Result;
            var inventories = response.Content.ReadAsAsync<Inventory>().Result;

            response = WebClient.ApiClient.GetAsync("Brands").Result;
            IList<Brand> brands = response.Content.ReadAsAsync<List<Brand>>().Result;
            var inventoryViewModel = new InventoryViewModel
            {
                ToolId = inventories.ToolId,
                Assetnumber = inventories.Assetnumber,
                Description = inventories.Description,
                Brandname = brands.Where(c => c.BrandId == inventories.BrandId).Select(u => u.Brand1).FirstOrDefault(),
                ActiveRetired = inventories.ActiveRetired,
                CheckedOut = inventories.CheckedOut,
                Comment = inventories.Comment,
                image = inventories.image,

            };
            return View(inventoryViewModel);
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Inventories/{id}").Result;
                TempData["SuccessMessage"] = "Action was Success";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Helper Methods

        /// <summary>
        /// This method will return the list of brands
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetBrands()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Brands").Result;
            IList<Brand> brands = response.Content.ReadAsAsync<IList<Brand>>().Result;

            List<SelectListItem> brandlist = brands.OrderBy(o => o.Brand1).Select(c => new SelectListItem
            {
                Value = c.BrandId.ToString(),
                Text = c.Brand1
            }).ToList();

            return new SelectList(brandlist,"Value", "Text");

        }

        /// <summary>
        /// This method will save the upload files to the folder
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), file.FileName));
            }
            return Json("File uploaded successfully");
        }

        #endregion
    }
}
