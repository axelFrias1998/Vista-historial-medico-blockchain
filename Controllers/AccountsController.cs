using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Vista_historial_medico_blockchain.Models;
using System.Net.Http.Headers;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class AccountsController : Controller
    { 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]

        public ActionResult Registrer()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AdminInfo(CreatedUserDTO createdUserDTO)
        {
            using(var client = new HttpClient()){
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var admins = new List<CreatedUserDTO>();
                var responseClinic = await client.GetAsync($"api/Accounts/GetAdmins/true");
                if (responseClinic.IsSuccessStatusCode)
                    admins.AddRange(JsonConvert.DeserializeObject<List<CreatedUserDTO>>(await responseClinic.Content.ReadAsStringAsync()).ToList());
                else
                    return NotFound();
                
                var responsePacs = await client.GetAsync($"api/Accounts/GetAdmins/false");
                if (responsePacs.IsSuccessStatusCode)
                    admins.AddRange(JsonConvert.DeserializeObject<List<CreatedUserDTO>>(await responsePacs.Content.ReadAsStringAsync()).ToList());
                else
                    return NotFound();
                return View(admins);
            }
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "Administrador hospital", Value = "0" });
            lst.Add(new SelectListItem() { Text = "Administrador clínica", Value = "1" });
            ViewBag.IdentityRol = lst;
            return View();
            
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateAdmin(UserAdminInfo userinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                string type = (userinfo.Type == 1) ? "true" : "false";
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var postTask = await client.PostAsJsonAsync<UserInfo>($"api/Accounts/CreateAdmin/{type}", userinfo);
                if (postTask.IsSuccessStatusCode)
                    return RedirectToAction("AdminInfo");
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("CreateAdmin");
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLogin userlogin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<UserLogin>("api/Accounts/Login", userlogin);

                if (postTask.IsSuccessStatusCode)
                {
                    var userToken = JsonConvert.DeserializeObject<UserToken>(await postTask.Content.ReadAsStringAsync());
                    SetCookie("Token", userToken.Token, userToken.Expiration);
                    return RedirectToAction("Index", "Panel");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Login");
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync($"api/Accounts/GetAccountInfo/{id}");
                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<CreatedUserDTO>(await response.Content.ReadAsStringAsync()));
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("Index", "Panel");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CreatedUserDTO createdUserDTO)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.PutAsJsonAsync($"api/Accounts/{createdUserDTO.id}", createdUserDTO);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("AdminInfo");
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("Index", "Panel");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrer(UserInfo userinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<UserInfo>("api/Accounts/CreatePacient", userinfo);
                if (postTask.IsSuccessStatusCode)
                    return File(await postTask.Content.ReadAsByteArrayAsync(), "text/plain", $"{userinfo.Nombre}_genNode.gti");
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("Registrer");
            }
        }

        [HttpGet]               
        public async Task<ActionResult> CreateDoctores()
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

                var response = await client.GetAsync($"api/HospitalSpecialities/{hospitalId}");
                if (response.IsSuccessStatusCode)
                {
                    List<SelectListItem> lst = new List<SelectListItem>();
                    foreach (var speciality in JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await response.Content.ReadAsStringAsync()).ToList())
                        lst.Add(new SelectListItem() { Text = $"{speciality.Type}", Value = $"{speciality.Id}"});
                    ViewBag.Especialidades = lst;
                    return View();
                }
                return NotFound();
            }
        }

        [HttpPost]               
        public async Task<ActionResult> CreateDoctores(DoctorInfoView doctorInfoView)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;

                var doctorInfo = new DoctorInfo
                {
                    Apellido = doctorInfoView.Apellido,
                    Email = doctorInfoView.Email,
                    Password = doctorInfoView.Password,
                    ConfirmPassword = doctorInfoView.ConfirmPassword,
                    Nombre = doctorInfoView.Nombre,
                    PhoneNumber = doctorInfoView.PhoneNumber,
                    UserName = doctorInfoView.UserName,
                    AdminId = tokens.Claims.First(claim => claim.Type == "UserId").Value,
                    EspecialidadId = Convert.ToInt32(doctorInfoView.EspecialidadId)
                };
                
                var postTask = await client.PostAsJsonAsync<DoctorInfo>("api/Accounts/CreateDoctor", doctorInfo);
                if (postTask.IsSuccessStatusCode)
                   return RedirectToAction("DoctoresHospital", "Hospitals");
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("CreateDoctores");
            }
        }

        public RedirectToActionResult LogOut()
        {
            Response.Cookies.Delete("Token");
            return RedirectToAction("Index", "Home");
        }

        private void SetCookie(string key, string value, DateTime expireTime)  
        {  
            CookieOptions option = new CookieOptions();  
            option.Expires = expireTime;
            Response.Cookies.Append(key, value, option);
        }
    }
}
