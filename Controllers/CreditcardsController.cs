using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_Store.Models;

namespace Market_Store.Controllers
{
    public class CreditcardsController : Controller
    {
        private readonly ModelContext _context;

        public CreditcardsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Creditcards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Creditcards.ToListAsync());
        }

        // GET: Creditcards/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcards
                .FirstOrDefaultAsync(m => m.Creditcardid == id);
            if (creditcard == null)
            {
                return NotFound();
            }

            return View(creditcard);
        }

        // GET: Creditcards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Creditcards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Creditcardid,Cardtype,Cardnumber,Expdata,Cvv,Modifieddate,Balance")] Creditcard creditcard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditcard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creditcard);
        }

        // GET: Creditcards/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcards.FindAsync(id);
            if (creditcard == null)
            {
                return NotFound();
            }
            return View(creditcard);
        }

        // POST: Creditcards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Creditcardid,Cardtype,Cardnumber,Expdata,Cvv,Modifieddate,Balance")] Creditcard creditcard)
        {
            if (id != creditcard.Creditcardid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditcard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditcardExists(creditcard.Creditcardid))
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
            return View(creditcard);
        }

        // GET: Creditcards/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcards
                .FirstOrDefaultAsync(m => m.Creditcardid == id);
            if (creditcard == null)
            {
                return NotFound();
            }

            return View(creditcard);
        }

        // POST: Creditcards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var creditcard = await _context.Creditcards.FindAsync(id);
            _context.Creditcards.Remove(creditcard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditcardExists(decimal id)
        {
            return _context.Creditcards.Any(e => e.Creditcardid == id);
        }
    }
}
