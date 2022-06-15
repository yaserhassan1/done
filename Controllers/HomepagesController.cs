﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market_Store.Models;

namespace Market_Store.Controllers
{
    public class HomepagesController : Controller
    {
        private readonly ModelContext _context;

        public HomepagesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Homepages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Homepages.ToListAsync());
        }

        // GET: Homepages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homepage = await _context.Homepages
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (homepage == null)
            {
                return NotFound();
            }

            return View(homepage);
        }

        // GET: Homepages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homepages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeId,Logoimage,Imgeslider1,TitleSlider1,TitleSlider2,Imageslider2")] Homepage homepage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homepage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homepage);
        }

        // GET: Homepages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homepage = await _context.Homepages.FindAsync(id);
            if (homepage == null)
            {
                return NotFound();
            }
            return View(homepage);
        }

        // POST: Homepages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("HomeId,Logoimage,Imgeslider1,TitleSlider1,TitleSlider2,Imageslider2")] Homepage homepage)
        {
            if (id != homepage.HomeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homepage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomepageExists(homepage.HomeId))
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
            return View(homepage);
        }

        // GET: Homepages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homepage = await _context.Homepages
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (homepage == null)
            {
                return NotFound();
            }

            return View(homepage);
        }

        // POST: Homepages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var homepage = await _context.Homepages.FindAsync(id);
            _context.Homepages.Remove(homepage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomepageExists(decimal id)
        {
            return _context.Homepages.Any(e => e.HomeId == id);
        }
    }
}
