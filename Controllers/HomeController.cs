using Market_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Market_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;


        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Username = HttpContext.Session.GetString("UserName");


            var product = _context.Productts.Include(p => p.Store.Cat).ToList();
            var category = _context.Categoryts.ToList();
            var service = _context.Services.ToList();
            var testimonial = _context.Testimonials.ToList();
            var homepage = _context.Homepages.ToList();




            //product-Clothing
            var product_Clothing = product.Where(cat => cat.Store.Cat.Categoryname == "Clothing").ToList();
            var product_Perfume = product.Where(cat => cat.Store.Cat.Categoryname == "Perfume").ToList();
            var product_Accessory = product.Where(cat => cat.Store.Cat.Categoryname == "accessory").ToList();
            var producFeatured = product.Where(r => Convert.ToDouble(r.Rating) >= 7.0).ToList();




            var tuple = Tuple.Create<

                IEnumerable<Market_Store.Models.Categoryt>,
                IEnumerable<Market_Store.Models.Productt>,
                IEnumerable<Market_Store.Models.Productt>,
                IEnumerable<Market_Store.Models.Productt>,
                IEnumerable<Market_Store.Models.Productt>,
                IEnumerable<Market_Store.Models.Testimonial>,
                IEnumerable<Market_Store.Models.Service>>(category, product_Clothing, product_Perfume, product_Accessory, producFeatured, testimonial, service);


            return View(tuple);
            
        }

     
        public IActionResult Shop()
        {
            var product = _context.Productts.Include(p => p.Store.Cat).ToList();
            return View(product);
        }

        public IActionResult Checkout()
        {
            var userid = HttpContext.Session.GetInt32("Userid");

            var shoppingCart =_context.Cartts.Include(p => p.Product).Include(u => u.User).Where(u => u.User.Userid == userid).ToList();

            return View(shoppingCart);
        }



        

        public IActionResult Cart()
        {

            var userid = HttpContext.Session.GetInt32("Userid");

            
            var cart = _context.Cartts.Where(id => id.UserId == userid).Include(id => id.Product).Include(id => id.User).ToList();



            return View(cart);
            
         
        }

        


        public IActionResult TryCart()
        {
            return View();
        }
        
        public JsonResult AddCart(int Productid)
        {
            var userid = HttpContext.Session.GetInt32("Userid");

            if(userid == null)
            {
                return Json(0);

            }
           
            Cartt cart = new Cartt();

            cart.Datecreated = DateTime.Now;
            cart.Quantity = 1;
            cart.ProductId = Convert.ToDecimal(Productid);
            cart.UserId = userid;
            _context.Add(cart);
            _context.SaveChanges();
            return Json(cart);
            
         
            
            

       

        }





        public async Task<IActionResult> PlaceOreder()
        {
            

            var userid = HttpContext.Session.GetInt32("Userid");

            var shoppingCart = _context.Cartts.Include(p => p.Product).Include(u => u.User).Where(u => u.User.Userid == userid).ToList();


            List<Ordert> orders = new List<Ordert>();

            foreach (var item in shoppingCart)
            {

                orders.Add(new Ordert
                {
                    Codename = "#" + item.CartId + RandomString(),
                    Prodid = item.ProductId,
                    Userid = item.UserId,
                    Quantity = item.Quantity,
                    Datecreated = DateTime.Now
                });

            }
            foreach (var item in orders)
            {
                _context.Orderts.Add(item);
                await _context.SaveChangesAsync();
            }

            var user = _context.Userts.Where(x => x.Userid == userid).FirstOrDefault();
            try
            {
                if (userid != null)
                {

                    using SmtpClient mySmtpClient = new SmtpClient("smtp.office365.com", 587);
                    mySmtpClient.EnableSsl = true;

                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    NetworkCredential basicAuthenticationInfo = new
                      NetworkCredential("osamaalessa@outlook.com", "osama12345");
                    mySmtpClient.Credentials = basicAuthenticationInfo;

                    // add from,to mailaddresses
                    //use your email
                    MailAddress from = new MailAddress("osamaalessa@outlook.com", "Market Store Admin");

                    //Send mail to:
                    //use customer email
                    MailboxAddress emailto = new MailboxAddress("user", user.Email);
                    MailAddress to = new MailAddress(user.Email, "Customer:");
                    using MailMessage myMail = new MailMessage(from, to);



                    // set subject and encoding
                    myMail.Subject = "Thank you for shopping";
                    myMail.SubjectEncoding = Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = "<h1>Hello</h1><h3>Order successfully</h3>";
                    myMail.BodyEncoding = Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;

                    await mySmtpClient.SendMailAsync(myMail);
                    Console.WriteLine("Email Has Been Send");
                }

            }

            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index","Home");
        }


        
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private string RandomString()
        {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = "0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;

        }

        

        public IActionResult TryShop()
        {
            return View();
        }


   

        public JsonResult ShowUsername()
        {
            var username = HttpContext.Session.GetString("UserName");
            return Json(username);
        }


        public IActionResult Testimonial()

        {
            return View();
        }

    }

    
}
