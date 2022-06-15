using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_Store.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Market_Store.Controllers
{
    public class CategorytsController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategorytsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Categoryts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoryts.ToListAsync());
        }

        // GET: Categoryts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryt = await _context.Categoryts
                .FirstOrDefaultAsync(m => m.Catid == id);
            if (categoryt == null)
            {
                return NotFound();
            }

            return View(categoryt);
        }

        // GET: Categoryts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoryts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Catid,Categoryname,Datecreated,Imagepath,ImageFile")] Categoryt categoryt)
        {
            if (ModelState.IsValid)
            {

                if (categoryt.ImageFile != null)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + categoryt.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {

                        await categoryt.ImageFile.CopyToAsync(fileStream);

                    }

                    categoryt.Imagepath = fileName;
                   


                }


                categoryt.Datecreated = DateTime.Now;
                _context.Add(categoryt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryt);
        }

        // GET: Categoryts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryt = await _context.Categoryts.FindAsync(id);
            if (categoryt == null)
            {
                return NotFound();
            }
            return View(categoryt);
        }

        // POST: Categoryts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,Categoryt categoryt)
        {
            if (id != categoryt.Catid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categoryt.Datecreated = DateTime.Now;


                    if (categoryt.ImageFile != null)
                    {

                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + categoryt.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {

                            await categoryt.ImageFile.CopyToAsync(fileStream);

                        }

                        categoryt.Imagepath = fileName;
                        _context.Update(categoryt);
                        await _context.SaveChangesAsync();


                    }
                   
                    _context.Update(categoryt);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorytExists(categoryt.Catid))
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
            return View(categoryt);
        }

        // GET: Categoryts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryt = await _context.Categoryts
                .FirstOrDefaultAsync(m => m.Catid == id);
            if (categoryt == null)
            {
                return NotFound();
            }

            return View(categoryt);
        }

        // POST: Categoryts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var categoryt = await _context.Categoryts.FindAsync(id);
            _context.Categoryts.Remove(categoryt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorytExists(decimal id)
        {
            return _context.Categoryts.Any(e => e.Catid == id);
        }
    }
}
