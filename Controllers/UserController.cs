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
    public class UserController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public UserController()
        {

        }

        public IActionResult AdminInfo()

        {

            return View();

        }

         public IActionResult CreateAdmin()

        {

            return View();

        }

         public IActionResult InfoCli()

        {

            return View();

        }

        public IActionResult Doctores()

        {

            return View();

        }

        public IActionResult CreateDoc()

        {

            return View();

        }



         public IActionResult Info()

        {

            return View();

        }


    }
}
