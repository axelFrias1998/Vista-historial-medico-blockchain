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

namespace Vista_historial_medico_blockchain.Controllers
{
    public class HomeController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<HomeController> _logger;
        
        HttpClient Client = new HttpClient();
        string url = "https://historial-blockchain.azurewebsites.net/api/Accounts/CreateAccount"; 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
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
                client.BaseAddress = new Uri("https://historial-blockchain.azurewebsites.net/");
                var postTask = client.PostAsJsonAsync<UserLogin>("api/Accounts/Login", userlogin);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var userToken = JsonConvert.DeserializeObject<UserToken>(await result.Content.ReadAsStringAsync());
                    SetCookie("Token", userToken.Token, userToken.Expiration);
                    /*var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(userToken.Token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var jti = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
                    
                    
                    var roles = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                    if (userToken is null)
                        return NotFound();   
                    if(roles == "Sysadmin")
                        RedirectToAction("Panel", "PanelsController", ViewBag.Role = "roles");  */          
                }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View(userlogin);
        }

        public void SetCookie(string key, string value, DateTime expireTime)  
        {  
            CookieOptions option = new CookieOptions();  
            option.Expires = expireTime;
            Response.Cookies.Append(key, value, option);  
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public async Task <IActionResult> Registrer([Bind("Apellido,Email,Password,ConfirmPassword,Nombre,PhoneNumber,UserName")] UserInfo UserInfo)
        {
            if (ModelState.IsValid)
            {
                await client.PostAsJsonAsync<UserInfo>(url, UserInfo);

                return RedirectToAction(nameof(Index));
            }
            return View(UserInfo);
        }*/

        public async Task<ActionResult> Registrer(UserInfo userinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://historial-blockchain.azurewebsites.net/");
                var postTask = client.PostAsJsonAsync<UserInfo>("api/Accounts/CreateAccount", userinfo);
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
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(userinfo);
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
