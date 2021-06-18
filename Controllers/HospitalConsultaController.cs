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

        public IActionResult MisConsultasP()
        {
            return View();
        }


        
        
        
    }
}
