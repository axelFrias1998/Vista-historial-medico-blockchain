using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vista_historial_medico_blockchain.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class UserController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public UserController()
        {

        }
        

        [HttpGet]
        public async Task<ActionResult> AdminInfo(CreatedUserDTO createdUserDTO)
        {
            using(var client = new HttpClient()){
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                string tipo = "true";
                /*if(){
                    tipo = "true";
                }else{
                    tipo = "false";
                }*/
                var response = await client.GetAsync($"api/Accounts/GetAdmins/{tipo}");
                if (response.IsSuccessStatusCode){
                    return View(JsonConvert.DeserializeObject<List<CreatedUserDTO>>(await response.Content.ReadAsStringAsync()).ToList());
                }
                else{
                    return NotFound();
                }
            }
            
        }

       

        public async Task<ActionResult> CreateAdmin(UserInfo userinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                List<SelectListItem> lst = new List<SelectListItem>();

                lst.Add(new SelectListItem() { Text = "PacsAdmin", Value = "0" });
                lst.Add(new SelectListItem() { Text = "ClinicAdmin", Value = "1" });

                ViewBag.IdentityRol= lst;
                string rol = userinfo.SelectedUsuario;
                string tipo = string.Empty;
                /*Request.Form["Id"].ToString()*/
                if(string.IsNullOrEmpty(rol)){
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View();
                }else{
                    if(rol.Equals("1")){
                        tipo = "true";
                    }
                    else if(rol.Equals("0")){
                        tipo = "false";
                    }
                    /*Mandar Token en el Header*/
                    var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                    var postTask = await client.PostAsJsonAsync<UserInfo>($"api/Accounts/CreateAdmin/{tipo}", userinfo);
                    if (postTask.IsSuccessStatusCode)
                    {
                            return RedirectToAction("AdminInfo");
                    }
                    else{
                        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                        return View("CreateAdmin");
                    }
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> AdminInfo(UserInfo userInfo)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                string id = "1";
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var postTask = await client.PutAsJsonAsync<UserInfo>($"api/Accounts/{id}", userInfo);
                if (postTask.IsSuccessStatusCode)
                            return RedirectToAction("AdminInfo");
                else
                {
                        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                        return RedirectToAction("AdminInfo");
                }
            }
        }

        [HttpGet]               
        public ActionResult EditAdmin(string id)
        {
            ViewData["Adminid"] = id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditAdmin(CreatedUserDTO createdUserDTO)
        {
            using (var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var updateUserDTO = new UpdateUserDTO
                {
                    nombre = createdUserDTO.nombre,
                    apellido = createdUserDTO.apellido,
                    userName = createdUserDTO.userName,
                    email = createdUserDTO.email,
                    phoneNumber = createdUserDTO.phoneNumber
                };
                var response = await client.PutAsJsonAsync<UpdateUserDTO>($"api/Accounts/{createdUserDTO.id}", updateUserDTO);
                if (response.IsSuccessStatusCode){
                    return RedirectToAction(@"AdminInfo");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("EditAdmin");
                } 
            }
        }

        public IActionResult InfoCli()
        {
            return View();
        }
                /*[HttpGet]
        public async Task<ActionResult> InfoCli(CreatedUserDTO createduserDTO)
        {
        using(var client = new HttpClient()){
        /*Mandar Token en el Header
        var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
        client.BaseAddress = new Uri("https://localhost:44349");
        var response = await client.GetAsync($"api/Accounts/GetAccountInfo/{createduserDTO.id}");
        if (response.IsSuccessStatusCode){
        return View(JsonConvert.DeserializeObject<CreatedUserDTO>(await client.GetStringAsync($"https://localhost:44349/api/Accounts/GetAccountInfo/{createduserDTO.id}")));
        }
        else{
        return NotFound();
        }
        }
        }*/

        /*
        public ViewResult InfoCli() => View();



        [HttpPost]
        public async Task<IActionResult> InfoCli( string id)
        {
        CreatedUserDTO createdUserDTO = new CreatedUserDTO();
        using (var httpClient = new HttpClient())
        {
        var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
        using (var response = await httpClient.GetAsync("https://localhost:44349/api/GetAccountInfo/" + id))
        {
        if (response.IsSuccessStatusCode)
        {
        string apiResponse = await response.Content.ReadAsStringAsync();
        createdUserDTO = JsonConvert.DeserializeObject<CreatedUserDTO>(apiResponse);
        }
        else
        ViewBag.StatusCode = response.StatusCode;
        }
        }
        return View(createdUserDTO);
        }*/


        [HttpGet]
        public async Task<ActionResult> Doctores (){
            using(var client = new HttpClient()){
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokenS = jsonToken as JwtSecurityToken;
                string idHospital = tokenS.Claims.First(claim => claim.Type == "HospitalId").Value;
                var response = await client.GetAsync($"api/HospitalDoctors/{idHospital}");
                if (response.IsSuccessStatusCode){
                    return View(JsonConvert.DeserializeObject<List<HospitalDoctorsInfoDTO>>(await response.Content.ReadAsStringAsync()).ToList());
                }
                else
                    return NotFound();
            }
        }

        public IActionResult ClinicAdmin()

        {

            return View();

        }

        public IActionResult CreateClinicAdmin()

        {

            return View();

        }

        public IActionResult CreateDoctores()

        {

            return View();

        }

              public IActionResult Pacientes()

        {

            return View();

        }

        public IActionResult Info()

        {

            return View();

        }


    }
}
