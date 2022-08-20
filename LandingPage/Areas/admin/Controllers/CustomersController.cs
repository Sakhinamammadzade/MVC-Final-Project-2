using LandingPage.Data;
using LandingPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers=_context.Customers.ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer,IFormFile myPhoto)
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);
            
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                myPhoto.CopyTo(fs);
            }
            customer.CustomerPhoto= "image/" + imageName;


            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
        
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(int id,Customer customer)
        {
            
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customers = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customers == null)
            {
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer,IFormFile myPhoto, string? oldPhoto)
        {

            if (myPhoto != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);

                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
                using (var fs = new FileStream(savePath, FileMode.Create))
                {
                    myPhoto.CopyTo(fs);
                }
                customer.CustomerPhoto = "image/" + imageName;
            }
            else
            {
                customer.CustomerPhoto = oldPhoto;
            }
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
            
    }
}
