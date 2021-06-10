using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelBindingSecureString.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            var plainText = SecureStringToString(vm.Password);

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 把 SecureString 轉回 PlainText
        /// </summary>
        private string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }

    public class ViewModel
    {
        public string Account { get; set; }

        [DataType(DataType.Password)]
        [BindProperty(BinderType = typeof(SecureStringBinder))]
        public SecureString Password { get; set; }
    }

    public class SecureStringBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var value = valueProviderResult.FirstValue;

            var propertyValue = new SecureString();

            foreach (var c in value)
            {
                propertyValue.AppendChar(c);
            }

            bindingContext.Result = ModelBindingResult.Success(propertyValue);

            return Task.CompletedTask;
        }
    }
}
