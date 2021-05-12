using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vista_historial_medico_blockchain.Controllers
{
    public class AccountsController : Controller
    {
        private readonly pubsContext _context;

        HttpClient client = new HttpClient();
        string url = "https://localhost:5001/api/Accounts/Register";

        public AccountsController(pubsContext context)
        {
            _context = context;
        }
        // POST: AccountsController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind("apellido,email,password,confirmPassword, nombre, phoneNumber,userName")] Resgitrer registrer)
        {
            if(ModelsState.IsValid)
            {
                await client.PostAsJsonAsync<Registrer>(url, registrer);
                return RedirectToAction(nameof(Registrer));
            }
            return view(registrer);
        }
        // POST: AccountsController/CreateAdimin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind("apellido,email,password,confirmPassword, nombre, phoneNumber,userName")] Admin admin)
        {
            if (ModelsState.IsValid)
            {
                await client.PostAsJsonAsync<Registrer>(url, admin);
                return RedirectToAction(nameof(Admin));
            }
            return view(admin);
        }

        // POST: AccountsController/CreateDoctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind("apellido,email,password,confirmPassword, nombre, phoneNumber,userName"),Bind("hospitalId")] Doctor doctor)
        {
            if (ModelsState.IsValid)
            {
                await client.PostAsJsonAsync<Registrer>(url, doctor);
                return RedirectToAction(nameof(Doctor));
            }
            return view(doctor);
        }

        // GET: AccountsController/Delete/5
        public async Task<IActionResult>Delete(string id)
        {
            if(id == null)
            {
                return NotFund();
            }
            var doctor = JsonConvert.DeserializarObjet<doctor>
                (await client.GetStringAsync(url + id));
            if (doctor == null)
            {
                return NotFound();
            }
            return view(doctor);
        }

        // POST: HomeController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await client.DeleteAsync(url + id);

            return RedirectToAction(nameof(Index));
        }
        private bool DoctorExists(string id)
        {
            return _context.Doctor.Any(e => e.Doctor_Id == id);
        }
    }
}
