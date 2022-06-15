using Market_Store.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Market_Store.Controllers
{
    public class UserDashboardController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserDashboardController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var userid = HttpContext.Session.GetInt32("Userid");
            var username = HttpContext.Session.GetInt32("UserName");

            ViewBag.numberOfUCarts = _context.Cartts.Where(id=>id.UserId==userid).Count();
            ViewBag.numberOfOrderts = _context.Orderts.Where(id=>id.Userid==userid).Count();
            ViewBag.sumOfPaid = _context.Orderts.Where(id=>id.Userid==userid).Sum(x => x.Prod.Price*x.Prod.Quantity);

            ViewBag.username = username;
            var user= _context.Userts.Find(Convert.ToDecimal(userid));

          


            return View();
        }


        public IActionResult ProfileUser()
        {
            var userid = HttpContext.Session.GetInt32("Userid");
            var user = _context.Userts.Find(Convert.ToDecimal(46));
            return View(user);
        }
    }
}
