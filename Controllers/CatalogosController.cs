using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public IActionResult Catalogos()

        {

            return View();

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
