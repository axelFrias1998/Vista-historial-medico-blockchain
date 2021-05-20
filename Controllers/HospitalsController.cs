using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Vista_historial_medico_blockchain.Models;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class HospitalsController : Controller
    {
        
        static readonly HttpClient client = new HttpClient();
        string url = "https://historial-blockchain20210512190841.azurewebsites.net/api/Hospitals";
        public HospitalsController()
        {

        }

       
        /*Get Hospital*/

         public async Task<ActionResult> Hospitales()
        {
            IEnumerable<HospitalInfo> hospitals = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://historial-blockchain.azurewebsites.net/");
                //sacar token de la cookie y mandarlo como BearerToken
                //sacar token de la cookie y mandarlo como BearerToken
                var result = await client.GetAsync("api/Hospitals");
                if (result.IsSuccessStatusCode)
                    hospitals = await result.Content.ReadFromJsonAsync<IList<HospitalInfo>>();
                else //web api sent error response 
                {
                    hospitals = Enumerable.Empty<HospitalInfo>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(hospitals);
        }



       /* public HospitalsController(historialblockchain_dbContext context)
        {
            _context = context;
        }*/

        // GET: Hospitals
        /*public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();
            
            return View(JsonConvert.DeserializeObject<List<Hospital>>(await client.GetStringAsync(url)).ToList());
        }*/

        public async Task<IActionResult> ServiceCaltalog ()
        {
            HttpResponseMessage response = await client.GetAsync("https://historial-blockchain.azurewebsites.net/api/Hospitals/GetCatalogOfServices");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var serviceCatalog = JsonConvert.DeserializeObject<List<ServicesCatalog>>(await response.Content.ReadAsStringAsync());

            if (serviceCatalog is null)
                return NotFound();
            ViewBag.CatalogoServicios = new SelectList(serviceCatalog, "Id", "Type");
            return View();
        }
         
        public async Task<ActionResult> CreateH(HospitalInfo hospitalinfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://historial-blockchain.azurewebsites.net/");
                var postTask = client.PostAsJsonAsync<HospitalInfo>("api/Accounts/Hospitals", hospitalinfo);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                  
                  return RedirectToAction("Index");
                }
            }

            HttpResponseMessage response = await client.GetAsync("https://historial-blockchain.azurewebsites.net/api/CatalogOfServices");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var serviceCatalog = JsonConvert.DeserializeObject<List<ServicesCatalog>>(await response.Content.ReadAsStringAsync());

            if (serviceCatalog is null)
                return NotFound();
            ViewBag.CatalogoServicios = new SelectList(serviceCatalog, "Id", "Type");

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(hospitalinfo);    
        }

        public IActionResult DetailsHos()

        {

            return View();

        }
        
         
        // GET: Hospitals/Details/5
       /* public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .Include(h => h.Admin)
                .Include(h => h.ServiceCatalog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // GET: Hospitals/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["ServiceCatalogId"] = new SelectList(_context.ServicesCatalogs, "Id", "Id");
            return View();
        }

        // POST: Hospitals/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
         //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,RegisterDate,ServiceCatalogId,AdminId")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.AspNetUsers, "Id", "Id", hospital.AdminId);
            ViewData["ServiceCatalogId"] = new SelectList(_context.ServicesCatalogs, "Id", "Id", hospital.ServiceCatalogId);
            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.AspNetUsers, "Id", "Id", hospital.AdminId);
            ViewData["ServiceCatalogId"] = new SelectList(_context.ServicesCatalogs, "Id", "Id", hospital.ServiceCatalogId);
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,PhoneNumber,RegisterDate,ServiceCatalogId,AdminId")] Hospital hospital)
        {
            if (id != hospital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalExists(hospital.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.AspNetUsers, "Id", "Id", hospital.AdminId);
            ViewData["ServiceCatalogId"] = new SelectList(_context.ServicesCatalogs, "Id", "Id", hospital.ServiceCatalogId);
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .Include(h => h.Admin)
                .Include(h => h.ServiceCatalog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(string id)
        {
            return _context.Hospitals.Any(e => e.Id == id);
        }*/
    }
}
