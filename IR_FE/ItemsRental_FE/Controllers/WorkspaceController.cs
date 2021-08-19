using ItemsRental_FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Controllers
{
    public class WorkspaceController : Controller
    {
        // GET: Workspace
        public ActionResult Index()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Workspaces").Result;

            // we are using IEnumerable because we only want to enumerate the collection and we are not going to add or delete any elements

            IEnumerable<Workspace> customers = response.Content.ReadAsAsync<IEnumerable<Workspace>>().Result;
            return View(customers);
        }

        // GET: Workspace/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Workspaces/{id}").Result;
            var workspace = response.Content.ReadAsAsync<Workspace>().Result;
            return View(workspace);
        }

        // GET: Workspace/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workspace/Create
        [HttpPost]
        public ActionResult Create(Workspace workspace)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Workspaces", workspace).Result;
                TempData["SuccessMessage"] = "Action was Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Workspace/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Workspaces/{id}").Result;
            var workspace = response.Content.ReadAsAsync<Workspace>().Result;
            //return Object to view
            return View(workspace);
        }

        // POST: Workspace/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Workspace workspace)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Workspaces/{id}", workspace).Result;
                TempData["SuccessMessage"] = "Action was Success";
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(workspace);
            }
            catch
            {
                return View();
            }
        }

        // GET: Workspace/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Workspaces/{id}").Result;
       
            var workspace = response.Content.ReadAsAsync<Workspace>().Result;

            return View(workspace);
        }

        // POST: Workspace/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Workspaces/{id}").Result;
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
