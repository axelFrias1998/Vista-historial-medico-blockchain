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
using System.IdentityModel.Tokens.Jwt;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class HospitalsController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<ActionResult> BorrarAdministrador(string adminId, string hospitalId)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.DeleteAsync($"api/HospitalAdministrador/{hospitalId}/{adminId}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("HospitalAdministradores", "Hospitals", hospitalId);
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<ActionResult> BorrarEspecialidad(string hospitalId, int specialityId)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.DeleteAsync($"api/HospitalSpecialities/{hospitalId}/{specialityId}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("HospitalEspecialidades", "Hospitals", new { id = hospitalId });
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> BorrarMedicamento(string id)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;
                string hospitalId = tokens.Claims.First(claim => claim.Type == "HospitalId").Value;

                var response = await client.DeleteAsync($"api/HospitalMedicamentos/{id}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("ListaMedicamentos", "Hospitals", new { hospitalId = hospitalId });
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> AgregarEspecialidad(string hospitalId)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/SpecialitiesCatalog");
                if (response.IsSuccessStatusCode)
                {
                    List<SelectListItem> lst = new List<SelectListItem>();
                    foreach (var speciality in JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await response.Content.ReadAsStringAsync()).ToList())
                        lst.Add(new SelectListItem() { Text = $"{speciality.Type}", Value = $"{speciality.Id}"});
                    ViewBag.Especialidades = lst;
                    var hospitalEspecialidad = new HospitalSpeciality
                    {
                        HospitalId = hospitalId
                    };
                    return View(hospitalEspecialidad);
                }
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> ListaMedicamentos(string hospitalId)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);

                var response = await client.GetAsync($"api/HospitalMedicamentos/{hospitalId}");
                if (response.IsSuccessStatusCode)
                {
                    var hospitalMedicamentos =  JsonConvert.DeserializeObject<List<HospitalMedicamentosDTO>>(await response.Content.ReadAsStringAsync()).ToList();
                    return View(hospitalMedicamentos);
                }
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> RegistrarMedicamento()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/CatalogoGrupoMedicamentos");
                if (response.IsSuccessStatusCode)
                {
                    List<SelectListItem> lst = new List<SelectListItem>();
                    foreach (var speciality in JsonConvert.DeserializeObject<List<CatalogoGrupoMedicamentos>>(await response.Content.ReadAsStringAsync()).ToList())
                        lst.Add(new SelectListItem() { Text = $"{speciality.Type}", Value = $"{speciality.Id}"});
                    ViewBag.GrupoMedicamentos = lst;
                    return View();
                }
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarMedicamento(HospitalMedicamentosCreateDTO hospitalMedicamentos)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/CatalogoGrupoMedicamentos");
                if (response.IsSuccessStatusCode)
                {
                    List<SelectListItem> lst = new List<SelectListItem>();
                    foreach (var speciality in JsonConvert.DeserializeObject<List<CatalogoGrupoMedicamentos>>(await response.Content.ReadAsStringAsync()).ToList())
                        lst.Add(new SelectListItem() { Text = $"{speciality.Type}", Value = $"{speciality.Id}"});
                    ViewBag.GrupoMedicamentos = lst;
                    return View();
                }
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AgregarEspecialidad(HospitalSpeciality hospitalSpeciality)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var map = new HospitalSpecialityAdd
                {
                    HospitalId = hospitalSpeciality.HospitalId,
                    EspecialidadId = Convert.ToInt32(hospitalSpeciality.EspecialidadId)
                };
                var response = await client.PostAsJsonAsync<HospitalSpecialityAdd>($"api/HospitalSpecialities", map);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("HospitalEspecialidades", "Hospitals", new { id = hospitalSpeciality.HospitalId });
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> AgregarAdministrador(string hospitalId)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/Hospitals/GetHospitalInfo/{hospitalId}");
                if (response.IsSuccessStatusCode)
                {
                    var hospitalInfo = JsonConvert.DeserializeObject<HospitalInfo>(await response.Content.ReadAsStringAsync());
                    var responseAvailableAdmins = (hospitalInfo.servicesCatalog.Id <= 2) ? await client.GetAsync($"api/HospitalAdministrador/AvailableAdmins/true") : await client.GetAsync($"api/HospitalAdministrador/AvailableAdmins/false");
                    if(!responseAvailableAdmins.IsSuccessStatusCode)
                        return NotFound();
                    var hospitalAdminsInfo = new HospitalAdminInfo
                    {
                        HospitalId = hospitalId
                    };
                    
                    List<SelectListItem> lst = new List<SelectListItem>();
                    foreach (var admin in JsonConvert.DeserializeObject<List<HospitalAdminDTO>>(await responseAvailableAdmins.Content.ReadAsStringAsync()).ToList())
                        lst.Add(new SelectListItem() { Text = $"{admin.Nombre} {admin.Apellido}", Value = admin.AdminId });
                    ViewBag.Admins = lst;
                    return View(hospitalAdminsInfo);
                }
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AgregarAdministrador(HospitalAdminInfo hospitalAdminInfo)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.PostAsJsonAsync<HospitalAdminInfo>($"api/HospitalAdministrador", hospitalAdminInfo);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Hospitals");
                }
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> HospitalAdministradores(string id)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/HospitalAdministrador/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var hospitalAdminsInfo = new HospitalAdminsInfo
                    {
                        Admins = JsonConvert.DeserializeObject<List<HospitalAdminDTO>>(await response.Content.ReadAsStringAsync()).ToList(),
                        HospitalId = id
                    };
                    return View(hospitalAdminsInfo);
                }
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> HospitalEspecialidades(string id)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/HospitalSpecialities/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var hospitalSpecialitiesInfo = new HospitalSpecialitiesInfo
                    {
                        Specialities = JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await response.Content.ReadAsStringAsync()).ToList(),
                        HospitalId = id
                    };
                    return View(hospitalSpecialitiesInfo);
                }
                return NotFound();                
            }
        }

        [HttpGet]
        public async Task<ActionResult> HospitalEspecialidadesAdministrador(string id)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/HospitalSpecialities/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var hospitalSpecialitiesInfo = new HospitalSpecialitiesInfo
                    {
                        Specialities = JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await response.Content.ReadAsStringAsync()).ToList(),
                        HospitalId = id
                    };
                    return View(hospitalSpecialitiesInfo);
                }
                return NotFound();                
            }
        }

        [HttpGet]
        public async Task<ActionResult> DoctoresHospital()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;
                string hospitalId = tokens.Claims.First(claim => claim.Type == "HospitalId").Value;
                var response = await client.GetAsync($"api/HospitalDoctors/{hospitalId}");
                if (response.IsSuccessStatusCode)
                {
                    var hospitalDoctors =  JsonConvert.DeserializeObject<List<HospitalDoctorsInfoDTO>>(await response.Content.ReadAsStringAsync()).ToList();
                    return View(hospitalDoctors);
                }
                return NotFound();                
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index()
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
                  return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Create");
                }
            }  
        }

        [HttpGet]
        public async Task<ActionResult> CrearMedicamento()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.GetAsync($"api/CatalogoGrupoMedicamentos");
                if (response.IsSuccessStatusCode)
                {
                    List<SelectListItem> lst = new List<SelectListItem>();
                    var gruposMedicamentos = JsonConvert.DeserializeObject<List<CatalogoGrupoMedicamentos>>(await response.Content.ReadAsStringAsync()).ToList();

                    var dropMedicamentos = new List<GrupoMedicamentosMap>();
                    foreach (var grupo in gruposMedicamentos)
                    {
                        dropMedicamentos.Add(new GrupoMedicamentosMap{
                            grupoMedicamentosId = grupo.Id,
                            type = grupo.Type
                        });
                    }
                    foreach (var grupo in dropMedicamentos)
                        lst.Add(new SelectListItem() { Text = $"{grupo.type}", Value = $"{grupo.grupoMedicamentosId}"});
                    ViewBag.GrupoMedicamentos = lst;
                    return View();
                }
                return NotFound();
            }
        }

        [HttpPost]               
        public async Task<ActionResult> CrearMedicamento(HospitalMedicamentosCreateDTO crearMedicamento)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;

                crearMedicamento.hospitalId = tokens.Claims.First(claim => claim.Type == "HospitalId").Value;
                var postTask = await client.PostAsJsonAsync<HospitalMedicamentosCreateDTO>("api/HospitalMedicamentos", crearMedicamento);

                if (postTask.IsSuccessStatusCode)
                {
                   return RedirectToAction("ListaMedicamentos", "Hospitals", new { hospitalId = crearMedicamento.hospitalId});
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
                var response = await client.PutAsync($"api/SpecialitiesCatalog/{specialitiesCatalog.Id}/{specialitiesCatalog.Type}", null);
                if (response.IsSuccessStatusCode){
                    return RedirectToAction(@"Especia");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("EditSpecia");
                } 
            }
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
        
        public async Task<ActionResult> InformacionHospital()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;

                string hospitalId = tokens.Claims.First(claim => claim.Type == "HospitalId").Value;
                var response = await client.GetAsync($"api/Hospitals/GetHospitalInfo/{hospitalId}");
                if (response.IsSuccessStatusCode)
                {
                    var hospitalInfo = JsonConvert.DeserializeObject<HospitalInfo>(await response.Content.ReadAsStringAsync());
                    return View(hospitalInfo);
                }
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> EliminarDoctor(string doctorId)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;

                string hospitalId = tokens.Claims.First(claim => claim.Type == "HospitalId").Value;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var response = await client.DeleteAsync($"api/HospitalDoctors/{hospitalId}/{doctorId}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("DoctoresHospital", "Hospitals", new { id = hospitalId });
                return NotFound();
            }
        }
        
    }
}
