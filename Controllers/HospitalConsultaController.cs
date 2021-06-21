using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
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
        [HttpGet]
        public async Task<ActionResult> CrearConsultaAsync(NodeInfo nodeInfo)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var response = await client.GetAsync($"api/HospitalConsultas/GetTransactions/{nodeInfo.GenNode}");
                if (response.IsSuccessStatusCode)
                {
                    var mongo = JsonConvert.DeserializeObject<List<TransactionBlock>>(await response.Content.ReadAsStringAsync()).ToList();
                    var consulaViewInfo = new ConsultaViewInfo
                    {
                        GenNode = nodeInfo.GenNode,
                        PacientId = nodeInfo.PacientId,
                        TransactionBlock = mongo
                    };
                    ViewData["transactions"] = consulaViewInfo;
                    return View();

                }
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CrearConsulta(CreateConsultaDTO createConsultaDTO)
        {
            Console.Write(createConsultaDTO);
            return View();
        }

       public IActionResult Registrer()
        {
            return View();
        }

       public ActionResult ValidacionArchivo()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidacionArchivo(PacientValidation pacientValidation) 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                using(var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(pacientValidation.File.OpenReadStream())
                    {
                        Headers =
                        {
                            ContentLength = pacientValidation.File.Length,
                            ContentType = new MediaTypeHeaderValue(pacientValidation.File.ContentType)
                        }
                    }, "file", pacientValidation.File.FileName);
                    var postTask = await client.PostAsync($"api/HospitalConsultas/GetNode/{pacientValidation.Username}/{pacientValidation.Password}", content);
                    if (postTask.IsSuccessStatusCode)
                    {
                        var nodeInfo = JsonConvert.DeserializeObject<NodeInfo>(await postTask.Content.ReadAsStringAsync());
                        if (nodeInfo is null)
                            return NotFound();
                        return RedirectToAction("CrearConsulta", nodeInfo);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                        return RedirectToAction("ValidacionArchivo");
                    }
                }
            }
        }

        public IActionResult MisConsultasP()
        {
            return View();
        } 

        public IActionResult VerConsultasDoctor()
        {
            return View();
        } 
    }
}

