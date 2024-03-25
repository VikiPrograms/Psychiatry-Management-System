using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using Humanizer.Localisation;
using DataLayer;
using DatabaseLayer;

namespace MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientContext patientContext;
        private readonly UserContext userContext;

        public PatientController(PatientContext patientContext, UserContext userContext)
        {
            this.patientContext = patientContext;
            this.userContext = userContext;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            return View(await patientContext.ReadAllAsync(true, true));
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await patientContext.ReadAsync((int)id, true);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            LoadNavigationalEntities();
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            await LoadNavigationalEntities();

            int age = int.Parse(formCollection["Age"]);

            Patient patient = new Patient(formCollection["Name"],
                age);

            if (ModelState.IsValid)
            {
                await patientContext.CreateAsync(patient);
                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await patientContext.ReadAsync((int)id);
            if (food == null)
            {
                return NotFound();
            }

            await LoadNavigationalEntities();

            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection formCollection)
        {
            if (id != int.Parse(formCollection["Id"]))
            {
                return NotFound();
            }

            int age = int.Parse(formCollection["Age"]);

            Patient patient = new Patient(formCollection["Name"],
                age);

            patient.PatientId = id;

            if (ModelState.IsValid)
            {
                try
                {
                    bool useNavigationalProperties = false;

                    string checkboxValue = formCollection["useNavigationalProperties"];
                    if (!string.IsNullOrEmpty(checkboxValue))
                    {
                        useNavigationalProperties = true;
                    }

                    await patientContext.UpdateAsync(patient, useNavigationalProperties);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PatientExists(patient.PatientId))
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

            await LoadNavigationalEntities();

            return View(patient);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await patientContext.ReadAsync((int)id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await patientContext.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadNavigationalEntities()
        {
            ViewData["UserId"] = new SelectList(await userContext.ReadAllUsersAsync(), "Id", "Username");
        }

        private async Task<bool> PatientExists(int id)
        {
            return await patientContext.ReadAsync(id) is not null;
        }
    }
}
