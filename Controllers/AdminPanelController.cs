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
    public class AdminPanelController : Controller
    {
        public IActionResult PanelAdmin()
        {
            return View();
        }

         public IActionResult AdminHospital()
        {
            return View();
        }

        public IActionResult DetailsHos()
        {
            return View();
        }

        public IActionResult Hospitales()
        {
            return View();
        }
    }
}