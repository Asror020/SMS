using Microsoft.IdentityModel.Tokens;
using SMS.BLL.Models.AuthenticationModels;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.AuthenticationServices.Interfaces
{
    public interface IJWTGeneratorService
    {
        Task<TokenDto> CreateToken(User user, bool populateExp);

        ClaimsIdentity GetIdentity(User user);

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}
