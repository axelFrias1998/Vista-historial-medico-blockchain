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

        public IActionResult Index()
        {
            /*var myObject = new SomeObject
            {
                SomeProperty = "someValue"
            };*/

            /*var objAsJson = new JavaScriptSerializer().Serialize(myObject);
            //var objAsJson = JsonConvert.SerializeObject(myObject);
            var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
            var _httpClient = new HttpClient();
            var result = await _httpClient.PutAsync("http://someDomain.com/someUrl", content); //or PostAsync for POST

            HttpResponseMessage response = await client.GetAsync("https://historial-blockchain20210512190841.azurewebsites.net/api/Hospitals/GetCatalogOfServices");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            Console.WriteLine(responseBody);*/
            return View();
        }
          public IActionResult Login()
        {
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<ActionResult> Login(UserLogin userlogin)
        {
            using (var client = new HttpClient())
            {
                /*Mandar Token en el Header
                 * var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                 * client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);*/
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
                        return View("../AdminPanel/PanelAdmin");
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
                        /*var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(userToken.Token);
                        var tokenS = jsonToken as JwtSecurityToken;
                        var rool = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;*/

                        return View("Login");
                    }
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Registrer");
                }
            }
        }
        
         public IActionResult Registrer()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
