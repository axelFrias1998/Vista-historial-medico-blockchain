using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Vista_historial_medico_blockchain.Models;
using System.Net.Http.Headers;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class HospitalsController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public IActionResult ListaMedicamentos()
        {
            return View();
        }

        public IActionResult CrearMedicamento()
        {
            return View();
        }

        [HttpPost]               
        public async Task<ActionResult> CrearMedicamento(HospitalMedicamentosCreateDTO crearMedicamento)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<HospitalMedicamentosCreateDTO>("api/HospitalMedicamentos", crearMedicamento);

                if (postTask.IsSuccessStatusCode)
                {
                   return RedirectToAction(@"ListaMedicamentos");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("CrearMedicamento");
                }
            }
        }

        [HttpGet]               
        public ActionResult EditarMedicamento(int id)
        {
            ViewData["EditarMedicamento"] = id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditarMedicamento(SpecialitiesCatalog specialitiesCatalog)
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
  
        /*Get Hospital*/
        [HttpGet]
        public IActionResult Hospitales(HospitalInfo hospitalInfo)
        {
            return View();
        }
        public async Task<IActionResult> ServiceCaltalog ()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api/Hospitals/GetCatalogOfServices");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return NotFound();
            var serviceCatalog = JsonConvert.DeserializeObject<List<ServicesCatalog>>(await response.Content.ReadAsStringAsync());
            if (serviceCatalog is null)
                return NotFound();
            ViewBag.CatalogoServicios = new SelectList(serviceCatalog, "Id", "Type");
            return View();
        }
         
        public async Task<ActionResult> CreateH(HospitalInfo hospitalinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = client.PostAsJsonAsync<HospitalInfo>("api/Accounts/Hospitals", hospitalinfo);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                  return RedirectToAction("Index");
                }
            }

            /*HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api/CatalogOfServices");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var serviceCatalog = JsonConvert.DeserializeObject<List<ServicesCatalog>>(await response.Content.ReadAsStringAsync());

            if (serviceCatalog is null)
                return NotFound();
            ViewBag.CatalogoServicios = new SelectList(serviceCatalog, "Id", "Type");

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");*/

            return View(hospitalinfo);   
        }

        public IActionResult DetailsHos()
        {
            return View();
        }     
    }
}
