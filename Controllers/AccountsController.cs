using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vista_historial_medico_blockchain.Models;
using Microsoft.AspNetCore.Identity;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class AccountsController : Controller
    { 
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
                    return RedirectToAction("PanelAdmin", "AdminPanel");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Login");
                }
            }
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
                        return NotFound();
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
