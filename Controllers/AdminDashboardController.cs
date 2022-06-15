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
using System.Threading.Tasks;

namespace Market_Store.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminDashboardController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.username = HttpContext.Session.GetString("UserName");
            ViewBag.id = HttpContext.Session.GetInt32("Userid");
            ViewBag.numberOfUser = _context.Userts.Count();
            ViewBag.numberOfProduct = _context.Productts.Count();
            ViewBag.numberOfStore = _context.Storets.Count();
            ViewBag.sumOfProduct = _context.Productts.Sum(x => x.Prodid);
            ViewBag.sumOfOrder = _context.Orderts.Count();
            ViewBag.countOfMale = _context.Userts.Count(x => x.Gender == "Male")-1;
            ViewBag.countOfFemale = _context.Userts.Count(x => x.Gender == "Female");

            var users = _context.Userts.ToList(); 

            //var user = _context.Userts.ToList();
            //var list = from b in user
            //           group b by b.Barthday into g
            //           select new
            //           {
            //               Age = g.ToList()                                 
            //            };

            //var listAge = new List<int>();


            
            //foreach (var item in list)
            //{
            //    listAge.Add(DateTime.Now.Year - item.Age[0].Barthday.Value.Year);

            //}
           
            //ViewBag.listOfAge = listAge.ToArray();


         
            return View(users);
        }

        public IActionResult Home()
        {

            return View();
        }

        public IActionResult Profile()
        {
            ViewBag.username = HttpContext.Session.GetString("UserName");
            var id = HttpContext.Session.GetInt32("Userid");


            var profile = _context.Userts.Find((decimal) id);


            return View(profile);

        }

        [HttpPost]
        public JsonResult Profile(Usert usert)
        {

            decimal id = (decimal) HttpContext.Session.GetInt32("Userid");

            var profile = _context.Userts.Find(id);


            if (id != usert.Userid)
            {
                return Json(0);
            }

            else if (usert.Password == null)
            {
                usert.Password = profile.Password;
            }
            else if (IsUsernameExist(usert.Username) == Json(1))
            {

                return Json(0);

            }

            else if (IsEmailExist(usert.Username) == Json(1))
            {
                return Json(0);
            }

                if (ModelState.IsValid)
                {

                    if (usert.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "$$" + usert.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {

                            
                            usert.ImageFile.CopyToAsync(fileStream);

                        }

                        usert.Imagepath = fileName;



                    }

                    _context.Update(usert);
                    _context.SaveChangesAsync();


                    return Json(1);
                }




                return Json(0);

        }



        public IActionResult Card()
        {
            var listCategory = _context.Categoryts.ToList();
            return View(listCategory);
        }

        
        
        public IActionResult Table()
        {
            var modelContext = _context.Productts.ToList();
            return View(modelContext);
        }

       

        public IActionResult date()
        {
            var modelContext = _context.Productts.ToList();
            return View(modelContext);
        }


        private bool UsertExists(decimal id)
        {
            return _context.Userts.Any(e => e.Userid == id);
        }

       

        public IActionResult tryPlug()

        {
            return View();
        }


        public JsonResult ShowUsername()
        {
            var username = HttpContext.Session.GetString("UserName");
            return Json(username);
        }

        public JsonResult IsUsernameExist(string username)
        {
            var isExits = _context.Userts.Where(s => s.Username == username).FirstOrDefault();

            if (isExits != null)
            {
                return Json(1);

            }
            else
            {
                return Json(0);
            }

        }

        public JsonResult IsEmailExist(string email)
        {
            var isExits = _context.Userts.Where(s => s.Email == email).FirstOrDefault();

            if (isExits != null)
            {
                return Json(1);

            }
            else
            {
                return Json(0);
            }

        }
    }
}
