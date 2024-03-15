using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidencePojistencu1.Data;
using EvidencePojistencu1.Models;

namespace EvidencePojistencu1.Controllers
{
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Insurance.Include(i => i.InsuredPerson);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.InsuredPerson)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurances/Create
        public IActionResult Create(int? insuredPersonId)
        {
            if (insuredPersonId == null)
            {
                return NotFound();
            }

            var insuredPerson = _context.InsuredPerson.FirstOrDefault(p => p.InsuredPersonId == insuredPersonId);
            if (insuredPerson == null)
            {
                return NotFound();
            }

            ViewData["InsuredPersonId"] = insuredPersonId;
            return View();
        }

        // POST: Insurances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceId,InsuranceType,PremiumAmount,StartDate,EndDate,InsuredPersonId")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance); 
                await _context.SaveChangesAsync(); 
                
                var insuredPerson = await _context.InsuredPerson
                    .Include(u => u.Insurances) // loaded insurances
                    .FirstOrDefaultAsync(u => u.InsuredPersonId == insurance.InsuredPersonId);
                //var insuredPerson = await _context.InsuredPerson.FirstOrDefaultAsync(p => p.InsuredPersonId == insurance.InsuredPersonId);

                if (insuredPerson != null)
                {
                    insuredPerson.Insurances.Add(insurance);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(insurance);
        }
        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewData["InsuredPersonId"] = new SelectList(_context.Set<InsuredPerson>(), "InsuredPersonId", "InsuredPersonId", insurance.InsuredPersonId);
            return View(insurance);
        }

        // POST: Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceId,InsuranceType,PremiumAmount,StartDate,EndDate,InsuredPersonId")] Insurance insurance)
        {
            if (id != insurance.InsuranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.InsuranceId))
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
            ViewData["InsuredPersonId"] = new SelectList(_context.Set<InsuredPerson>(), "InsuredPersonId", "InsuredPersonId", insurance.InsuredPersonId);
            return View(insurance);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.InsuredPerson)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurance.Remove(insurance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurance.Any(e => e.InsuranceId == id);
        }
    }
}
