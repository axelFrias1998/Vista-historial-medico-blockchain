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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class HospitalConsultaController : Controller
    { 
        [HttpGet]
        public async Task<ActionResult> ConsultasHospital()
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;
                string hospitalId = tokens.Claims.First(claim => claim.Type == "HospitalId").Value;
                
                var response = await client.GetAsync($"api/HospitalConsultas/{hospitalId}");
                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<List<HospitalConsultasDTO>>(await response.Content.ReadAsStringAsync()).ToList());
                return NotFound();
            }
        } 

        [HttpGet]
        public async Task<ActionResult> ConsultasDoctor()
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(ck);
                var tokens = jsonToken as JwtSecurityToken;
                string hospitalId = tokens.Claims.First(claim => claim.Type == "UserId").Value;
                
                var response = await client.GetAsync($"api/HospitalConsultas/Doctor/{hospitalId}");
                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<List<HospitalConsultasDTO>>(await response.Content.ReadAsStringAsync()).ToList());
                return NotFound();
            }
        } 

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

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadJwtToken(ck);
                    var tokens = jsonToken as JwtSecurityToken;
                    string hospitalId = tokens.Claims.First(claim => claim.Type == "HospitalId").Value;

                    var responseMedicamentos = await client.GetAsync($"api/HospitalMedicamentos/{hospitalId}");
                    if (responseMedicamentos.IsSuccessStatusCode)
                    {
                        List<SelectListItem> lst = new List<SelectListItem>();
                        foreach (var medicamentosDTO in JsonConvert.DeserializeObject<List<HospitalMedicamentosDTO>>(await responseMedicamentos.Content.ReadAsStringAsync()).ToList())
                            lst.Add(new SelectListItem() { Text = $"{medicamentosDTO.nombreMedicamento}", Value = $"{medicamentosDTO.nombreMedicamento}"});
                        
                        var consultaInfo = new CreateConsultaDTO
                        {
                            PacienteId = nodeInfo.PacientId,
                            GenNodeId = nodeInfo.GenNode,
                            HospitalId = hospitalId,
                            DoctorId = tokens.Claims.First(claim => claim.Type == "UserId").Value
                        };
                        ViewBag.Medicamentos = lst;
                        return View(consultaInfo);
                    }
                    return NotFound();

                }
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearConsulta(CreateConsultaDTO createConsultaDTO)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349");
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                var postTask = await client.PostAsJsonAsync<CreateConsultaDTO>("api/HospitalConsultas", createConsultaDTO);
                if (postTask.IsSuccessStatusCode)
                  return RedirectToAction("CrearConsulta", "HospitalConsulta", new NodeInfo { GenNode = createConsultaDTO.GenNodeId, PacientId = createConsultaDTO.PacienteId});
                else
                {
                    return RedirectToAction("Index", "Panel");
                }
            }
        }

        public IActionResult Registrer()
        {
            return View();
        }

        public ActionResult ValidacionArchivo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ValidacionArchivoPaciente()
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

        [HttpPost]
        public async Task<ActionResult> ValidacionArchivoPaciente(PacientValidation pacientValidation) 
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
                        return RedirectToAction("MisConsultas", "HospitalConsulta", nodeInfo);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                        return RedirectToAction("ValidacionArchivoPaciente");
                    }
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> MisConsultas(NodeInfo nodeInfo)
        {
            using(var client = new HttpClient())
            {
                var ck = ControllerContext.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ck);
                client.BaseAddress = new Uri("https://localhost:44349");
                
                var response = await client.GetAsync($"api/HospitalConsultas/MisConsultas/{nodeInfo.GenNode}/{nodeInfo.PacientId}");
                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<MiCalendarioConsultasDTO>(await response.Content.ReadAsStringAsync()));
                return NotFound();
            }
        }
    }
}

