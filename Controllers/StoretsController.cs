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
    public class StoretsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoretsController(ModelContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        // GET: Storets
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Storets.Include(s => s.Cat);
            return View(await modelContext.ToListAsync());
        }

        // GET: Storets/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storet = await _context.Storets
                .Include(s => s.Cat)
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (storet == null)
            {
                return NotFound();
            }

            return View(storet);
        }

        // GET: Storets/Create
        public IActionResult Create()
        {
            ViewData["Catid"] = new SelectList(_context.Categoryts, "Catid", "Categoryname");
            return View();
        }

        // POST: Storets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Storet storet)
        {
            if (ModelState.IsValid)
            {
                if (storet.ImageFile != null)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString()+"$$"+storet.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath +"/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {

                        await storet.ImageFile.CopyToAsync(fileStream);

                    }

                    storet.Imagepath = fileName;

                    _context.Add(storet);


                }
                _context.Add(storet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Catid"] = new SelectList(_context.Categoryts, "Catid", "Categoryname", storet.Catid);
            return View(storet);
        }

        // GET: Storets/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storet = await _context.Storets.FindAsync(id);
            if (storet == null)
            {
                return NotFound();
            }
            ViewData["Catid"] = new SelectList(_context.Categoryts, "Catid", "Categoryname", storet.Catid);
            return View(storet);
        }

        // POST: Storets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  Storet storet)
        {
            if (id != storet.Storeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (storet.ImageFile != null)
                    {

                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "$$" + storet.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {

                            await storet.ImageFile.CopyToAsync(fileStream);

                        }

                        storet.Imagepath = fileName;

                   

                    }
                    _context.Update(storet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoretExists(storet.Storeid))
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
            ViewData["Catid"] = new SelectList(_context.Categoryts, "Catid", "Categoryname", storet.Catid);
            return View(storet);
        }

        // GET: Storets/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storet = await _context.Storets
                .Include(s => s.Cat)
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (storet == null)
            {
                return NotFound();
            }

            return View(storet);
        }

        // POST: Storets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var storet = await _context.Storets.FindAsync(id);
            _context.Storets.Remove(storet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoretExists(decimal id)
        {
            return _context.Storets.Any(e => e.Storeid == id);
        }
    }
}
