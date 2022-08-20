using LandingPage.Models;
using Microsoft.EntityFrameworkCore;

namespace LandingPage.Data
{
    public class DataSeeding
    {
        private readonly AppDbContext _context;

        public DataSeeding(AppDbContext context)
        {
            _context = context;
        }
        public void SeedData()
        {
            if (_context.Database.GetPendingMigrations().Count() == 0)
            {
                if(_context.Services.Count() == 0)
                {
                    _context.Services.AddRange(Services);
                }
                _context.SaveChanges();

            }
        }


        public static Service[] Services =
        {
        new Service()
        {
             Icon="fa-brands fa-facebook",
             Title ="Fully Responsive",
             Description="This theme will look great on any device, no matter the size!"

        },
        new Service()
        {
              Icon="fa-brands fa-instagram",
             Title ="Fully Responsive",
             Description="This theme will look great on any device, no matter the size!"
        },
        new Service()
        {
             Icon="fa-brands fa-twitter",
             Title ="Fully Responsive",
             Description="This theme will look great on any device, no matter the size!"}

      };


    }
}
