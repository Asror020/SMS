using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Models.Configurations
{
    public class AuthenticationConfigurations
    {
        public static string Position => "AuthenticationConfigurations";

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public string Key { get; set; } = null!;

        public int Lifetime { get; set; }
    }
}
