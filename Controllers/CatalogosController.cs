using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Vista_historial_medico_blockchain.Models;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class CatalogosController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public CatalogosController()
        {

        }
        public async Task<ActionResult> Catalogos()

        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api/CatalogOfServices");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();
            
            return View(JsonConvert.DeserializeObject<List<ServicesCatalog>>(await client.GetStringAsync("https://localhost:44349/api/CatalogOfServices")).ToList());             
        }

        public async Task<ActionResult> Especia()

        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api/SpecialitiesCatalog");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();
            
            return View(JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await client.GetStringAsync("https://localhost:44349/api/SpecialitiesCatalog")).ToList());
                      

        }


        /*public async Task<ActionResult>  CreateSpecialities( ServicesCatalog serviceCatalog)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = client.PostAsJsonAsync<UserInfo>("api/Accounts/CreateAccount", userinfo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Especia");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(userinfo);
        }*/
    
       

        public IActionResult Specialities()

        {
            
            return View();

        }

        
        /*[HttpPost]
        [ValidateAntiForgeryToken]*/
         public async Task<ActionResult> CreateSpecialities(SpecialitiesCatalog specialitiescatalog)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = client.PostAsJsonAsync<SpecialitiesCatalog>("api/SpecialitiesCatalog", specialitiescatalog);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var userToken = JsonConvert.DeserializeObject<UserToken>(await result.Content.ReadAsStringAsync());
                }
            }
            return View(specialitiescatalog);
        }
    }
}