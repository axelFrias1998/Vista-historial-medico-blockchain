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
    public class HospitalConsultaController : Controller
    { 
        public IActionResult CrearConsulta()
        {
            return View();
        }

       public IActionResult Registrer()
        {
            return View();
        }

       public IActionResult ValidacionArchivo()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidacionArchivo(PacientValidation pacientValidation) 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var postTask = await client.PostAsJsonAsync<PacientValidation>("api/HospitalConsultas/GetNode", pacientValidation);
                if (postTask.IsSuccessStatusCode)
                {
                    var userToken = JsonConvert.DeserializeObject<UserToken>(await postTask.Content.ReadAsStringAsync());
                    if (userToken is null)
                        return NotFound();
                    return View("Login");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View("Registrer");
                }
            }
        }

        public IActionResult MisConsultasP()
        {
            return View();
        } 
    }
}

