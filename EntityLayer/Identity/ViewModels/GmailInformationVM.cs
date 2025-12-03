using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Identity.ViewModels
{
    public class GmailInformationVM
    {
        public string Email { get; set; } = null!;
        public string Host { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
