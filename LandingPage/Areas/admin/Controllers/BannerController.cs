using LandingPage.Data;
using LandingPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Areas.admin.Controllers
{
    [Area("admin")]
     [Authorize]
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;

        public BannerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var banner=_context.Banners.FirstOrDefault();
            return View(banner);
        }

        public IActionResult Create()
        {
            var banner = _context.Banners.FirstOrDefault();
            if(banner != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Banner banner,IFormFile myPhoto)
        {

            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);
            //get url
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                myPhoto.CopyTo(fs);
            }
            banner.PhotoUrl = "image/" + imageName;



            _context.Banners.Add(banner);   
            _context.SaveChanges();
           return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var banner=_context.Banners.FirstOrDefault();
            if(banner == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(Banner banner)
        {
            if(banner == null)
            {
                return RedirectToAction("Index");
            }
            _context.Banners.Remove(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var banner=_context.Banners.FirstOrDefault(x=>x.Id==id);   
            if(banner == null)
            {
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        public IActionResult Edit(Banner banner,IFormFile myPhoto, string? oldPhoto)
        {
            if (myPhoto != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);
           
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
                using (var fs = new FileStream(savePath, FileMode.Create))
                {
                    myPhoto.CopyTo(fs);
                }
                banner.PhotoUrl = "image/" + imageName;
            }
            else
            {
                banner.PhotoUrl = oldPhoto;
            }
            _context.Banners.Update(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
