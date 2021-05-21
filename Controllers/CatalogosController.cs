
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Vista_historial_medico_blockchain.Models;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class CatalogosController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public CatalogosController()
        {

        }
        public async Task<ActionResult> Catalogos()

        {
            HttpResponseMessage response = await client.GetAsync("https://historial-blockchain.azurewebsites.net/api/CatalogOfServices");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();
            
            return View(JsonConvert.DeserializeObject<List<ServicesCatalog>>(await client.GetStringAsync("https://historial-blockchain.azurewebsites.net/api/CatalogOfServices")).ToList());
                      

        }

        public async Task<ActionResult> Especia()

        {
            HttpResponseMessage response = await client.GetAsync("https://historial-blockchain.azurewebsites.net/api/SpecialitiesCatalog");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();
            
            return View(JsonConvert.DeserializeObject<List<SpecialitiesCatalog>>(await client.GetStringAsync("https://historial-blockchain.azurewebsites.net/api/SpecialitiesCatalog")).ToList());
                      

        }


    
        public IActionResult CreateSpecialities()

        {

            return View();

        }

        public IActionResult Specialities()

        {

            return View();

        }
    }
}