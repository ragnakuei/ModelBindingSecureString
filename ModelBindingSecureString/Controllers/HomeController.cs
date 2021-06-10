using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelBindingSecureString.Models;
using ModelBindingSecureString.Helpers;

namespace ModelBindingSecureString.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ViewModel vm)
        {
            var plainText = SecureStringHelper.ToString(vm.Password);

            return View(vm);
        }
    }
}
