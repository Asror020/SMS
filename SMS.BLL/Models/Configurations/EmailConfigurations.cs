using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Models.Configurations
{
    public class EmailConfigurations 
    {
        public static string Position => "EmailConfigurations";
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Port { get; set; }
        public string Host { get; set; } = null!;
    }
}
