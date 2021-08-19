using ItemsRental_FE.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Brands").Result;
            IEnumerable<Brand> brands = response.Content.ReadAsAsync<IEnumerable<Brand>>().Result;
            return View(brands);
        }

        // GET: Brand/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Brands/{id}").Result;
            var brand = response.Content.ReadAsAsync<Brand>().Result;
            return View(brand);
        }

        // GET: Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Brands", brand).Result;
                TempData["SuccessMessage"] = "Action was Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Brands/{id}").Result;
            var brand = response.Content.ReadAsAsync<Brand>().Result;
            return View(brand);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Brand brand)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Brands/{id}", brand).Result;
                TempData["SuccessMessage"] = "Action was Success";
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View(brand);
            }
            catch
            {
                return View();
            }
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Brands/{id}").Result;
            var brand = response.Content.ReadAsAsync<Brand>().Result;
            return View(brand);
        }

        // POST: Brand/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Brand brand)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Brands/{id}").Result;
                TempData["SuccessMessage"] = "Action was Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
