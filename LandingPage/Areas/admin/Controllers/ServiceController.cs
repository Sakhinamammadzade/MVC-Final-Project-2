using LandingPage.Data;
using LandingPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var service=_context.Services.ToList();
            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
  
        public IActionResult Create(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if(id == null)
            {
                return RedirectToAction("Índex");
            }
            var service=_context.Services.FirstOrDefault(x => x.Id == id);
            return View(service);
        }
        [HttpPost]
        public IActionResult Delete(Service service)
        {
          
            _context.Services.Remove(service);
            _context.SaveChanges();
           return  RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var service=_context.Services.FirstOrDefault(x=>x.Id==id);
            if (service == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(service);   

        }

        [HttpPost]
        public IActionResult Edit(Service service)
        {
           _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
