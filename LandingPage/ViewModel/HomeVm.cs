using LandingPage.Models;

namespace LandingPage.ViewModel
{
    public class HomeVm
    {
        public Banner banner { get; set; }
        public List<Service> Services { get; set; }
        public List<About> Abouts { get; set; }
        public List<Customer> Customers { get; set; }
        public Sign SignUp { get; set; }

    }
}
