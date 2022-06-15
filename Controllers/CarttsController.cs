using Market_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Market_Store.Controllers
{
    public class CarttsController : Controller
    {
        private readonly ModelContext _context;

        public CarttsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Cartts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Cartts.Include(c => c.Product).Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Cartts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartt = await _context.Cartts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartt == null)
            {
                return NotFound();
            }

            return View(cartt);
        }

        // GET: Cartts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Productts, "Prodid", "Productname");
            ViewData["UserId"] = new SelectList(_context.Userts, "Userid", "Email");
            return View();
        }

        // POST: Cartts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,ProductId,UserId,Quantity,Datecreated")] Cartt cartt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Productts, "Prodid", "Productname", cartt.ProductId);
            ViewData["UserId"] = new SelectList(_context.Userts, "Userid", "Email", cartt.UserId);
            return View(cartt);
        }

        // GET: Cartts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartt = await _context.Cartts.FindAsync(id);
            if (cartt == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Productts, "Prodid", "Productname", cartt.ProductId);
            ViewData["UserId"] = new SelectList(_context.Userts, "Userid", "Email", cartt.UserId);
            return View(cartt);
        }

        // POST: Cartts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("CartId,ProductId,UserId,Quantity,Datecreated")] Cartt cartt)
        {
            if (id != cartt.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarttExists(cartt.CartId))
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
            ViewData["ProductId"] = new SelectList(_context.Productts, "Prodid", "Productname", cartt.ProductId);
            ViewData["UserId"] = new SelectList(_context.Userts, "Userid", "Email", cartt.UserId);
            return View(cartt);
        }

        // GET: Cartts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartt = await _context.Cartts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartt == null)
            {
                return NotFound();
            }

            return View(cartt);
        }

        // POST: Cartts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var cartt = await _context.Cartts.FindAsync(id);
            _context.Cartts.Remove(cartt);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart","Home");
        }

        private bool CarttExists(decimal id)
        {
            return _context.Cartts.Any(e => e.CartId == id);
        }


        public JsonResult DeleteItem(int cartid)
        {

            var cartt = _context.Cartts.Find((decimal) cartid);
            _context.Cartts.Remove(cartt);
            _context.SaveChangesAsync();

            return Json(new { link="/Home/Cart" });
        }

        public JsonResult Updateqty(int cartid, int quantity)
        {

            var cartt = _context.Cartts.Find((decimal)cartid);

            cartt.Quantity = quantity;

            _context.Cartts.Update(cartt);
            _context.SaveChangesAsync();


            return Json(cartt);
        }
    }
}
