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

        [HttpGet]               
        public ActionResult CreateDoctores()
        {
            return View();
        }

        [HttpPost]               
        public async Task<ActionResult> CreateDoctores(DoctorInfo doctorInfo)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<DoctorInfo>("api/Accounts/CreateDoctor", doctorInfo);
                if (postTask.IsSuccessStatusCode)
                {
                   return RedirectToAction(@"Doctores");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("CreateDoctores");
                }
            }
        }

        /*public async Task<ActionResult> CreateDoctores(DoctorInfo doctorinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var postTask = await client.PostAsJsonAsync<DoctorInfo>("api/Accounts/CreateDoctor", doctorinfo);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Doctores");
                    }
                    else{
                        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                        return View("CreateDoctores");
                    }
            }
        }*/

        public IActionResult ClinicAdmin()

        {

            return View();

        }

        public IActionResult CreateClinicAdmin()

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
