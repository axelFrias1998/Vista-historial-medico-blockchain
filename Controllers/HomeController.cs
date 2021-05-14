using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vista_historial_medico_blockchain.Models;

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
        public IActionResult Registrer()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PanelAdmin()
        {
            return View();
        }

         public IActionResult AdminHospital()
        {
            return View();
        }

         public IActionResult CreateH()
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
