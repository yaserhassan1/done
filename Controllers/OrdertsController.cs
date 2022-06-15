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
    public class OrdertsController : Controller
    {
        private readonly ModelContext _context;

        public OrdertsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orderts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Orderts.Include(o => o.Prod).Include(o => o.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orderts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordert = await _context.Orderts
                .Include(o => o.Prod)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (ordert == null)
            {
                return NotFound();
            }

            return View(ordert);
        }

        // GET: Orderts/Create
        public IActionResult Create()
        {
            ViewData["Prodid"] = new SelectList(_context.Productts, "Prodid", "Productname");
            ViewData["Userid"] = new SelectList(_context.Userts, "Userid", "Email");
            return View();
        }

        // POST: Orderts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,Codename,Userid,Prodid,Datecreated,Quantity,DateFrom,DateTo")] Ordert ordert)
        {
            if (ModelState.IsValid)
            {
                ordert.Datecreated = DateTime.Now;
                
                if(ordert.Quantity==null || ordert.Quantity == 0)
                {
                    ordert.Quantity = 1;
                }
                _context.Add(ordert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Prodid"] = new SelectList(_context.Productts, "Prodid", "Productname", ordert.Prodid);
            ViewData["Userid"] = new SelectList(_context.Userts, "Userid", "Email", ordert.Userid);
            return View(ordert);
        }

        // GET: Orderts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordert = await _context.Orderts.FindAsync(id);
            if (ordert == null)
            {
                return NotFound();
            }
            ViewData["Prodid"] = new SelectList(_context.Productts, "Prodid", "Productname", ordert.Prodid);
            ViewData["Userid"] = new SelectList(_context.Userts, "Userid", "Email", ordert.Userid);
            return View(ordert);
        }

        // POST: Orderts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Orderid,Codename,Userid,Prodid,Datecreated,Quantity,DateFrom,DateTo")] Ordert ordert)
        {
            if (id != ordert.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ordert.Datecreated = DateTime.Now;

                    if (ordert.Quantity == null || ordert.Quantity == 0)
                    {
                        ordert.Quantity = 1;
                    }
                    _context.Update(ordert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdertExists(ordert.Orderid))
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
            ViewData["Prodid"] = new SelectList(_context.Productts, "Prodid", "Productname", ordert.Prodid);
            ViewData["Userid"] = new SelectList(_context.Userts, "Userid", "Email", ordert.Userid);
            return View(ordert);
        }

        // GET: Orderts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordert = await _context.Orderts
                .Include(o => o.Prod)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (ordert == null)
            {
                return NotFound();
            }

            return View(ordert);
        }

        // POST: Orderts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var ordert = await _context.Orderts.FindAsync(id);
            _context.Orderts.Remove(ordert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdertExists(decimal id)
        {
            return _context.Orderts.Any(e => e.Orderid == id);
        }
    }
}
