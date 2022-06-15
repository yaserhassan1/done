using Market_Store.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Market_Store.Controllers

{
    public class LoginRegisterationController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginRegisterationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Login()
        {
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Username = HttpContext.Session.GetString("UserName");

            ViewData["mess"] = ViewData["alert"];
            return View();

        }

        [HttpPost]
        public IActionResult Login(Usert login)
        {

            var auth = _context.Userts.Where(e=> e.Username == login.Username && e.Password==login.Password).FirstOrDefault();
            

            if(auth!=null)
            {


                HttpContext.Session.SetInt32("Userid", (int) auth.Userid);
                HttpContext.Session.SetString("UserName",auth.Username);

                if(Convert.ToInt32(auth.Roleid)== 1)
                {
                    return RedirectToAction("Index", "Home");
                }

                else if(Convert.ToInt32(auth.Roleid) == 2)
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }

            }
             
            return View();

        }


        public IActionResult Register()
        {
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Username = HttpContext.Session.GetString("UserName");

        

            return View();
        }

        [HttpPost]
        public IActionResult Register(Usert register)
        {
   
           

            if (ModelState.IsValid)
            {
                if (IsUsernameExist(register.Username) == Json(1))
                {

                    return View(register);

                }

                else if (IsEmailExist(register.Username) == Json(1))
                {
                    return View(register);
                }

                else
                {
                    if (register.ImageFile != null)
                    {

                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + register.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {

                            register.ImageFile.CopyToAsync(fileStream);

                        }

                        register.Imagepath = fileName;
                        _context.Add(register);


                    }


                  

                 


                    register.Datecreated = DateTime.Now;
                    register.Roleid = 1;
                    register.State = "Active";
                    _context.Add(register);
                    _context.SaveChangesAsync();


                   

                    return RedirectToAction("Login", "LoginRegisteration");
                }



            }




            ViewData["alert"] = "Done";

           

            return View(register);






        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "LoginRegisteration");
        }


        public JsonResult IsUsernameExist(string username)
        {
            var isExits = _context.Userts.Where(s=>s.Username==username).FirstOrDefault();

            if(isExits != null)
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


        public JsonResult ChangePassword(string oldPassword, string newPassword)
        {

            var id = 46;//HttpContext.Session.GetInt32("Userid");


            var find = _context.Userts.Find((decimal)id);

            if (oldPassword == find.Password)
            {
                find.Password = newPassword;

                _context.Userts.Update(find);

                _context.SaveChangesAsync();

                return Json(1);
            }
            else
            {
                return Json(0);

            }











        }


    }
}
