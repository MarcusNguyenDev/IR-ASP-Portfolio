using ItemsRental_FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {

            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customers").Result;

            // we are using IEnumerable because we only want to enumerate the collection and we are not going to add or delete any elements

            IEnumerable<Customer> customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{id}").Result;
            //Get a customer based on the ID 
            var customer = response.Content.ReadAsAsync<Customer>().Result;
            // return the customer object to the View
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Customers", customer).Result;
                TempData["SuccessMessage"] = "Action was Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{id}").Result;
            //Get a customer based on the ID 
            var customer = response.Content.ReadAsAsync<Customer>().Result;

            //return Object to view
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Customers/{id}", customer).Result;
                TempData["SuccessMessage"] = "Action was Success";

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(customer);


            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{id}").Result;
            //Get a customer based on the ID 
            var customer = response.Content.ReadAsAsync<Customer>().Result;

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Customers/{id}").Result;
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
