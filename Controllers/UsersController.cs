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
    public class UsersController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public UsersController()
        {

        }
        //public UsersController(historialblockchain_dbContext context)
        //{
        //    _context = context;
        //}

        // GET: AspNetUsers
        //public async Task<IActionResult> Index()
        //{
        //    var historialblockchain_dbContext = _context.AspNetUsers.Include(a => a.Hospital);
        //    return View(await historialblockchain_dbContext.ToListAsync());
        //}
        // GET: Users/Details
        public async Task<IActionResult> Index ()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api/Hospitals/GetCatalogOfServices");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var serviceCatalog = JsonConvert.DeserializeObject<List<ServicesCatalog>>(await response.Content.ReadAsStringAsync());

            if (serviceCatalog is null)
                return NotFound();

            foreach (var service in serviceCatalog)
            {
                Console.WriteLine($"id: {service.Id}, Tipo: {service.Type}, Es público: {service.IsPublic}");
            }
            ViewBag.CatalogoServicios = new SelectList(serviceCatalog, "Id", "Name");
            ViewBag.Hola = "Soy Axel";
            
            return View();
        }



        // GET: AspNetUsers/Create
        /*public IActionResult Create()
        {
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id");
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HospitalId,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Apellido,Nombre")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", aspNetUser.HospitalId);
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", aspNetUser.HospitalId);
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,HospitalId,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Apellido,Nombre")] AspNetUser aspNetUser)
        {
            if (id != aspNetUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserExists(aspNetUser.Id))
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
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", aspNetUser.HospitalId);
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUsers
                .Include(a => a.Hospital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUser = await _context.AspNetUsers.FindAsync(id);
            _context.AspNetUsers.Remove(aspNetUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }*/
    }
}
