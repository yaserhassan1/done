using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_Store.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Market_Store.Controllers
{
    public class ProducttsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProducttsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
    
        // GET: Productts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Productts.Include(p => p.Store.Cat);

            
            
            return View(await modelContext.ToListAsync());
        }

        // GET: Productts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productt = await _context.Productts
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Prodid == id);
            if (productt == null)
            {
                return NotFound();
            }

            return View(productt);
        }

        // GET: Productts/Create
        public IActionResult Create()
        {
            ViewData["Storeid"] = new SelectList(_context.Storets, "Storeid", "Storename");
            return View();
        }

        // POST: Productts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Productt productt)
        {
            if (ModelState.IsValid)
            {
                if (productt.ImageFile != null)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() +"_" + productt.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {

                        await productt.ImageFile.CopyToAsync(fileStream);

                    }

                    productt.Imagepath = fileName;
                    _context.Add(productt);
                    await _context.SaveChangesAsync();

                }

                //if Qty not has value
                if (productt.Quantity == null)
                {
                    productt.Quantity = (decimal )0;
                }

                //Time Create 
                productt.Datecreated = DateTime.Now;


                _context.Add(productt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Storeid"] = new SelectList(_context.Storets, "Storeid", "Storename", productt.Storeid);
            return View(productt);
        }

        // GET: Productts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productt = await _context.Productts.FindAsync(id);
            if (productt == null)
            {
                return NotFound();
            }
            ViewData["Storeid"] = new SelectList(_context.Storets, "Storeid", "Storename", productt.Storeid);
            return View(productt);
        }

        // POST: Productts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  Productt productt)
        {
            if (id != productt.Prodid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (productt.ImageFile != null)
                    {

                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + productt.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {

                            await productt.ImageFile.CopyToAsync(fileStream);

                        }

                        productt.Imagepath = fileName;
                        _context.Update(productt);
                        await _context.SaveChangesAsync();

                    }

                    //if Qty not has value
                    if (productt.Quantity == null)
                    {
                        productt.Quantity = (decimal) 0;
                    }

                    //Time Create 
                    productt.Datecreated = DateTime.Now;

                    _context.Update(productt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducttExists(productt.Prodid))
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
            ViewData["Storeid"] = new SelectList(_context.Storets, "Storeid", "Storename", productt.Storeid);
            return View(productt);
        }

        // GET: Productts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productt = await _context.Productts
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Prodid == id);
            if (productt == null)
            {
                return NotFound();
            }

            return View(productt);
        }

        // POST: Productts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productt = await _context.Productts.FindAsync(id);
            _context.Productts.Remove(productt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducttExists(decimal id)
        {
            return _context.Productts.Any(e => e.Prodid == id);
        }
    }
}
