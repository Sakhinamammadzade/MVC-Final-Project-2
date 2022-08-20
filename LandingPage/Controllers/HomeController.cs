using LandingPage.Data;
using LandingPage.Models;
using LandingPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LandingPage.Controllers
{
    public class HomeController : Controller
    {
        //private readonly DataSeeding _dataSeeding;
        private readonly AppDbContext _context;

        //public HomeController(DataSeeding dataSeeding)
        //{
        //    _dataSeeding = dataSeeding;
        //}

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //_dataSeeding.SeedData();

            var banner=_context.Banners.FirstOrDefault();
            var services = _context.Services.ToList();
            var abouts=_context.Abouts.ToList();
            var Customers=_context.Customers.ToList();

           HomeVm homeVm = new HomeVm()
            {
                banner = banner,
                Services = services,
                Abouts = abouts,
                Customers=Customers


            };
            return View(homeVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmail(Sign sign)
        {
            _context.Signs.Add(sign);
            await _context.SaveChangesAsync();
            return Ok();

        }



    }
}