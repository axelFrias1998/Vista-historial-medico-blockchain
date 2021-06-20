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

        [HttpGet]
        public async Task<ActionResult> Hospitales()
        {
            using(var client = new HttpClient()){
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync($"api/Hospitals");
                if (response.IsSuccessStatusCode){
                    return View(JsonConvert.DeserializeObject<List<HospitalInfo>>(await response.Content.ReadAsStringAsync()).ToList());
                }
                else
                    return NotFound();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Hospital público", Value = "1" });
            lst.Add(new SelectListItem() { Text = "Hospital privado", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Clínica pública", Value = "3" });
            lst.Add(new SelectListItem() { Text = "Clínica privada", Value = "4" });
            
            ViewBag.ServiceCatalog = lst;
            return View();
        }     

        [HttpPost]
        public async Task<ActionResult> Create(HospitalInfo hospitalinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var postTask = await client.PostAsJsonAsync<HospitalInfo>("api/Hospitals", hospitalinfo);
                if (postTask.IsSuccessStatusCode)
                {
                  return RedirectToAction("Hospitales");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Create");
                }
            }  
        }

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

        [HttpGet]
        public async Task<ActionResult> ModalEspecialidades(string idSpecialities){
            using(HttpClient client = new HttpClient()){
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/HospitalSpecialities/{idSpecialities}");
                if (response.IsSuccessStatusCode){
                    return View("ModalEspecialidades",JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await response.Content.ReadAsStringAsync()).ToList());
                }
                else{
                    return NotFound();
                }
                
            }
        }

        [HttpGet]
        public async Task<ActionResult> ModalAdministrador(string idHospital){
            using(HttpClient client = new HttpClient()){
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/HospitalAdministrador/{idHospital}");
                if (response.IsSuccessStatusCode){
                    return View("ModalAdministrador",JsonConvert.DeserializeObject<List<CreatedUserDTO>>(await response.Content.ReadAsStringAsync()).ToList());
                }
                else{
                    return NotFound();
                }
                
            }
        }

        public IActionResult GetViewEspecialities(string id){
            return RedirectToAction("ModalEspecialidades",new{ idSpecialities = id});
        }

        public IActionResult GetViewAdmin(string id)
        {
            return RedirectToAction("ModalAdministrador",new{ idHospital = id});
        }

        public async Task<IActionResult> ServiceCaltalog()
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
         
        

        public IActionResult DetailsHos()
        {
            return View();
        } 
        
        
    }
}
