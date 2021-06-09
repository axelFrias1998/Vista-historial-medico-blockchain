using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Vista_historial_medico_blockchain.Models;
using System.Net.Http.Headers;
namespace Vista_historial_medico_blockchain.Controllers
{
    public class CatalogosController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public CatalogosController()
        {

        }
        
        [HttpGet]
        public async Task<ActionResult> Catalogos(ServicesCatalog servicesCatalog)
        {
            using(var client = new HttpClient()){
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync("api/CatalogOfServices");
                if (response.IsSuccessStatusCode){
                    return View(JsonConvert.DeserializeObject<List<ServicesCatalog>>(await client.GetStringAsync("https://localhost:44349/api/CatalogOfServices")).ToList());  ;
                }
                else{
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> Especia(SpecialitiesCatalog specialitiesCatalog)
        {
           using(var client = new HttpClient()){
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync("api/SpecialitiesCatalog");
                if (response.IsSuccessStatusCode){
                     return View(JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await client.GetStringAsync("https://localhost:44349/api/SpecialitiesCatalog")).ToList());
                }
                else{
                    return NotFound();
                }
            }
        }

        [HttpGet]               
        public ActionResult CreateSpecialities()
        {
            return View();
        }

        [HttpPost]               
        public async Task<ActionResult> CreateSpecialities(CreateSpeciality createSpeciality)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<CreateSpeciality>("api/SpecialitiesCatalog", createSpeciality);

                if (postTask.IsSuccessStatusCode)
                {
                   return RedirectToAction(@"Especia");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("CreateSpecialities");
                }
            }
        }

        
        [HttpGet]               
        public ActionResult EditSpecia(int id)
        {
            ViewData["Specialityid"] = id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditSpecia(SpecialitiesCatalog specialitiesCatalog)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.PutAsync($"api/SpecialitiesCatalog/{specialitiesCatalog.Id}/{specialitiesCatalog.Nombre}", null);
                if (response.IsSuccessStatusCode){
                    return RedirectToAction(@"Especia");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("EditSpecia");
                } 
            }
        }

            [HttpGet]
       /* public async Task<ActionResult> Especia(SpecialitiesCatalog specialitiesCatalog)

        {
           using(var client = new HttpClient()){
                
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync($"api/SpecialitiesCatalog/{specialitiesCatalog.Id}");
                if (response.IsSuccessStatusCode){
                     return View(JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await client.GetStringAsync("https://localhost:44349/api/api/SpecialitiesCatalog/{specialitiesCatalog.Id}")).ToList());  ;
                }
                else{
                    return NotFound();
                }
            }
        }*/

     [HttpDelete]
     public async Task<ActionResult> Delete(SpecialitiesCatalog specialitiesCatalog)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.DeleteAsync($"api/SpecialitiesCatalog/{specialitiesCatalog.Id}");
                if (response.IsSuccessStatusCode){
                     return RedirectToAction(@"Especia");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Especia");
                } 
            }
        }

       
    }
}