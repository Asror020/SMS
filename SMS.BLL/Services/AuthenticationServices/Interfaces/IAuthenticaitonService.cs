using SMS.BLL.Models.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.AuthenticationServices.Interfaces
{
    public interface IAuthenticaitonService
    {
        Task<TokenDto> SignIn(SignInModel model);
    }
}
