using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Net.Http.Headers;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class UserController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public UserController()
        {

        }

        public async Task<ActionResult> AdminInfo(CreatedUserDTO createdUserDTO)
        {
            using(var client = new HttpClient()){
                /*Mandar Token en el Header*/
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync("api/Accounts/GetAdmins/true");
                if (response.IsSuccessStatusCode){
                    return View(JsonConvert.DeserializeObject<List<CreatedUserDTO>>(await response.Content.ReadAsStringAsync()).ToList());
                }
                else{
                    return NotFound();
                }
            }
            
            //USEN DISPOSABLES CON USING O RESPONSE.DISPOSE();
            //HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api​/Accounts​/GetAdmins​/" AGREGAR BOOLEANO);
            //response.Headers.Add("BearerToken", obtener cookie)
            //if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            //    return NotFound();
            //var userToken = JsonConvert.DeserializeObject<UserToken>(await response.Content.ReadAsStringAsync());
 //
            //var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(userToken.Token);
            //var tokenS = jsonToken as JwtSecurityToken;
            //var jti = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
//
            //if (userToken is null)
            //    return NotFound();
            //return View(JsonConvert.DeserializeObject<List<CreatedUserDTO>>(await response.Content.ReadAsStringAsync()).ToList());
        }

       

        public async Task<ActionResult> CreateAdmin(UserInfo userinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = client.PostAsJsonAsync<UserInfo>("api/Accounts/CreateAdmin/{type}", userinfo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var userToken = JsonConvert.DeserializeObject<UserToken>(await result.Content.ReadAsStringAsync());
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(userToken.Token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var jti = tokenS.Claims.First(claim => claim.Type == "UserId").Value;

                    if (userToken is null)
                    return NotFound();
                    return RedirectToAction("AdminInfo");
                }

            HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api/Users/GetRoles");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var identityRol = JsonConvert.DeserializeObject<List<IdentityRol>>(await response.Content.ReadAsStringAsync());

            if (identityRol is null)
                return NotFound();
            ViewBag.IdentityRol = new SelectList(identityRol, "id", "name");

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(userinfo); 
            }
        }


         public IActionResult InfoCli()

        {

            return View();

        }

        public IActionResult ClinicAdmin()

        {

            return View();

        }

        public IActionResult CreateClinicAdmin()

        {

            return View();

        }

        public IActionResult Doctores()

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
