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
using System.Net.Http.Headers;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class HomeController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrer()
        {
            return View();
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
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadJwtToken(userToken.Token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    string rool = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

                    /*if (rool.Equals("SysAdmin"))
                        /*RedirectToAction("PanelAdmin");*/
                        /*return View("../Views/AdminPanel/PanelAdmin.cshtml");*/
                    if(rool.Equals("SysAdmin")){
                        return View("../AdminPanel/PanelAdmin");
                    }else if(rool.Equals("ClinicAdmin")){
                        return View("../AdminPanel/ClinicAdmin");
                    }else if(rool.Equals("PacsAdmin")){
                        return View("../AdminPanel/ClinicAdmin");
                    }else if(rool.Equals("Doctor")){
                        return View("../AdminPanel/DoctorPanel");
                    }else if(rool.Equals("Pacient")){
                        return View("../AdminPanel/ClinicAdmin");
                    }else{
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Login");
                }
            }
        }

        public void SetCookie(string key, string value, DateTime expireTime)  
        {  
            CookieOptions option = new CookieOptions();  
            option.Expires = expireTime;
            Response.Cookies.Append(key, value, option);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrer(UserInfo userinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<UserInfo>("api/Accounts/CreateAccount", userinfo);

                if (postTask.IsSuccessStatusCode)
                {
                    var userToken = JsonConvert.DeserializeObject<UserToken>(await postTask.Content.ReadAsStringAsync());
                    if (userToken is null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        SetCookie("Token", userToken.Token, userToken.Expiration);
                        return View("Login");
                    }
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Registrer");
                }
            }
        }
        
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
