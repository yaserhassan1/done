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
using Microsoft.AspNetCore.Http;

namespace Market_Store.Controllers
{
    public class UsertsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsertsController(ModelContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Userts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Userts.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Userts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usert = await _context.Userts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (usert == null)
            {
                return NotFound();
            }


            return View(usert);
        }

        // GET: Userts/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: Userts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fullname,Email,Imagepath,Username,Password,Barthday,Phonenumber,Gender,Datecreated,State,Roleid,ImageFile")] Usert usert)
        {
            if (ModelState.IsValid)
            {

                if (usert.ImageFile != null)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + usert.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {

                        await usert.ImageFile.CopyToAsync(fileStream);

                    }

                    usert.Imagepath = fileName;
                    _context.Add(usert);
                   

                }


                usert.Datecreated = DateTime.Now;
                usert.Roleid = 1;
                usert.State = "Active";
                _context.Add(usert);
                await _context.SaveChangesAsync();

                ViewBag.IsVaild=1;

                return RedirectToAction(nameof(Index));
			}
            
            return View(usert);
        }

        // GET: Userts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usert = await _context.Userts.FindAsync(id);
            if (usert == null)
            {
                return NotFound();
            }
           
            return View(usert);
        }

        // POST: Userts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Email,Imagepath,Username,Password,Barthday,Phonenumber,Gender,Datecreated,State,Roleid,ImageFile")] Usert usert)
        {
            if (id != usert.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (usert.ImageFile != null)
                    {

                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "$$" + usert.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {

                            await usert.ImageFile.CopyToAsync(fileStream);

                        }

                        usert.Imagepath = fileName;
                       

                    }


                    _context.Update(usert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsertExists(usert.Userid))
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
            
            return View(usert);
        }

        // GET: Userts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usert = await _context.Userts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (usert == null)
            {
                return NotFound();
            }

            return View(usert);
        }

        // POST: Userts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var usert = await _context.Userts.FindAsync(id);
            _context.Userts.Remove(usert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsertExists(decimal id)
        {
            return _context.Userts.Any(e => e.Userid == id);
        }

        private JsonResult CheckUsernameAvailability(string username)
        {
            System.Threading.Thread.Sleep(200);

            var searchUsername = _context.Userts.Where(e => e.Username == username).SingleOrDefault();
            if(searchUsername!=null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }


        public IActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reg(Usert usert)
        {
            if (ModelState.IsValid)
            {

                if (usert.ImageFile != null)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + usert.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {

                        await usert.ImageFile.CopyToAsync(fileStream);

                    }

                    usert.Imagepath = fileName;
                    _context.Add(usert);


                }


                usert.Datecreated = DateTime.Now;
                usert.Roleid = 1;
                usert.State = "Active";
                _context.Add(usert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(usert);
           
        }

        
        public JsonResult IsVaild(int val)
        {
            return Json(val);

        }

    }
}
