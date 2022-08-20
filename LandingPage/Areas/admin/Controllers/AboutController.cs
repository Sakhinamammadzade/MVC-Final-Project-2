using LandingPage.Data;
using LandingPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class AboutController : Controller
     {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var about=_context.About.ToList();

            return View(about);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(About about,IFormFile myPhoto)
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);

            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                myPhoto.CopyTo(fs);
            }
            about.PhotoUrl = "image/" + imageName;
            _context.About.Add(about);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
                 
            }
            var about = _context.About.FirstOrDefault(a => a.Id == id);
            return View(about);
        }

        [HttpPost]
        public IActionResult Delete(int id,About about)
        {
            _context.About.Remove(about);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public IActionResult Edit(int id)
        {
            if(id== null)
            {
                return NotFound();
            }
            var about = _context.About.FirstOrDefault(a => a.Id == id);
            if (about == null)
            {
                return RedirectToAction("Index");
            }
            return View(about);
        }

        [HttpPost]
        public IActionResult Edit(About about,IFormFile myPhoto, string? oldPhoto)
        {
            if (myPhoto != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);

                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
                using (var fs = new FileStream(savePath, FileMode.Create))
                {
                    myPhoto.CopyTo(fs);
                }
                about.PhotoUrl = "image/" + imageName;

            }
            else
            {
                about.PhotoUrl = oldPhoto;
            }
            _context.About.Update(about);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
