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
    public class PanelsController : Controller
    {
        public IActionResult Menu()
        {
            string cookie = Request.Cookies["Token"];

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(cookie);
            var tokens = jsonToken as JwtSecurityToken;
            
            var loggedUser = new LoggedUser()
            {
                Username =  tokens.Claims.First(claim => claim.Type == "unique_name").Value,
                Rol = tokens.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value
            };
            return View(loggedUser);
        }
        
        public IActionResult Seleccion()
        {
            return View();
        }
    }
}