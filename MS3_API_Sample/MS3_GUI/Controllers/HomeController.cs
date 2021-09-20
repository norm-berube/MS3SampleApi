using Google.Api.Gax.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MS3_GUI.Models;
using System.Net.Http;

namespace MS3_GUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            IEnumerable<Identification> clients = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62695/api/");
                var responseTask = client.GetAsync("Clients");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Identification>>();
                    readTask.Wait();

                    clients = readTask.Result;
                }
                else //web api sent error response 
                {
                    clients = Enumerable.Empty<Identification>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            //return View(students);
            return View(clients);
        }


        public ActionResult Details(int id)
        {
            Identification indClient = GetSpecificClient(id);

            return View(indClient);

        }

        public ActionResult Edit(int id)
        {
            Identification indClient = GetSpecificClient(id);

            return View(indClient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Identification model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62695/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Identification>("Clients", model);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }


        public ActionResult Delete(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62695/api/");

                var deleteTask = client.DeleteAsync("Clients/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");


        }

        private Identification GetSpecificClient(int id)
        {
            Identification indClient = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62695/api/");

                var responseTask = client.GetAsync("Clients/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Identification>();
                    readTask.Wait();

                    indClient = readTask.Result;
                }
                else //web api sent error response 
                {
                    indClient = new Identification();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return indClient;
        }
       
        
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}