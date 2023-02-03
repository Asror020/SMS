using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Models.AuthenticationModels
{
    public class TokenDto
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public ClaimsPrincipal Principal { get; set; } = null!;
    }
}
