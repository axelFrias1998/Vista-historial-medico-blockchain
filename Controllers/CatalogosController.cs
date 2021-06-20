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

        [HttpGet]
        public async Task<ActionResult> Servicios(ServicesCatalog servicesCatalog)
        {
            using(var client = new HttpClient()){
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync("api/CatalogOfServices");
                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<List<ServicesCatalog>>(await response.Content.ReadAsStringAsync()).ToList());
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Especialidades()
        {
            using(var client = new HttpClient()){
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync("api/SpecialitiesCatalog");
                if (response.IsSuccessStatusCode)
                     return View(JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await response.Content.ReadAsStringAsync()).ToList());
                return NotFound();
            }
        }

        [HttpGet]               
        public ActionResult CrearEspecialidad()
        {
            return View();
        }

        [HttpPost]               
        public async Task<ActionResult> CrearEspecialidad(CreateSpeciality createSpeciality)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<CreateSpeciality>("api/SpecialitiesCatalog", createSpeciality);
                if (postTask.IsSuccessStatusCode)
                   return RedirectToAction("Especialidades");
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("CrearEspecialidad");
            }
        }
        
        [HttpGet]               
        public ActionResult EditarEspecialidad(int id)
        {
            ViewData["Specialityid"] = id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditarEspecialidad(SpecialitiesCatalog specialitiesCatalog)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.PutAsync($"api/SpecialitiesCatalog/{specialitiesCatalog.EspecialidadId}/{specialitiesCatalog.Nombre}", null);
                if (response.IsSuccessStatusCode){
                    return RedirectToAction("Especialidades");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("EditSpecia");
                } 
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> BorrarEspecialidad(SpecialitiesCatalog specialitiesCatalog)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.DeleteAsync($"api/SpecialitiesCatalog/{specialitiesCatalog.EspecialidadId}");
                if (response.IsSuccessStatusCode){
                     return RedirectToAction("Especialidades");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Especialidades");
                } 
            }
        }

        [HttpGet]
        public async Task<ActionResult> Medicamentos(ListadoGrupoMedicamentosDTO listadogrupoMedicamentos)
        {
           using(var client = new HttpClient())
           {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync("api/CatalogoGrupoMedicamentos");
                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<List<ListadoGrupoMedicamentosDTO>>(await client.GetStringAsync("https://localhost:44349/api/CatalogoGrupoMedicamentos")).ToList());
                return NotFound();
            }
        }

        [HttpGet]               
        public ActionResult CreateTipoMedicamento()
        {
            return View();
        }

        [HttpPost]               
        public async Task<ActionResult> CreateTipoMedicamento(CreateTipoMedicamento createtipoMedicamento)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<CreateTipoMedicamento>("api/CatalogoGrupoMedicamentos", createtipoMedicamento);

                if (postTask.IsSuccessStatusCode)
                {
                   return RedirectToAction(@"Medicamentos");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("CreateTipoMedicamento");
                }
            }
        }
        
        [HttpGet]               
        public ActionResult EditTipoMedicamento(int id)
        {
            ViewData["Medicamentoid"] = id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditTipoMedicamento(ListadoGrupoMedicamentosDTO listadogrupoMedicamentos)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.PutAsync($"api/CatalogoGrupoMedicamentos/{listadogrupoMedicamentos.Id}/{listadogrupoMedicamentos.Type}", null);
                if (response.IsSuccessStatusCode){
                    return RedirectToAction(@"Medicamentos");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("EditTipoMedicamentos");
                } 
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteMedicamentos(ListadoGrupoMedicamentosDTO listadogrupoMedicamentos)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.DeleteAsync($"api/CatalogoGrupoMedicamentos/{listadogrupoMedicamentos.Id}");
                if (response.IsSuccessStatusCode){
                     return RedirectToAction(@"Medicamentos");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Medicamentos");
                } 
            }
        }

        public IActionResult EleccionMedicamentos()  
        {
            return View();
        }  
    }
}