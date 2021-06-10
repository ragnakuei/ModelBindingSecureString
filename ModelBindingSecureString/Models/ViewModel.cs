using System.ComponentModel.DataAnnotations;
using System.Security;
using Microsoft.AspNetCore.Mvc;
using ModelBindingSecureString.Infra;

namespace ModelBindingSecureString.Models
{
    public class ViewModel
    {
        public string Account { get; set; }

        [DataType(DataType.Password)]
        [BindProperty(BinderType = typeof(SecureStringBinder))]
        public SecureString Password { get; set; }
    }
}