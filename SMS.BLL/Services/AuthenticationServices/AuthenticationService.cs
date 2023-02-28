using SMS.BLL.Models.AuthenticationModels;
using SMS.BLL.Services.AuthenticationServices.Interfaces;
using SMS.BLL.Services.EntityServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticaitonService
    {
        private readonly IUserService _userService;
        private readonly IJWTGeneratorService _jwtGeneratorService;
        public AuthenticationService(IUserService userService, IJWTGeneratorService jwtGeneratorService)
        {
            _userService = userService;
            _jwtGeneratorService = jwtGeneratorService;
        }

        public async Task<TokenDto> SignIn(SignInModel model)
            {
            if(model == null) return new TokenDto();

            var user = await _userService.GetByEmailAsync(model.Email);

            if (user == null ||
                user.Password != model.Password ||
                user.IsEmailVerified == false ||
                user.IsUserConfirmed == false
                ) return new TokenDto();

            var token = await _jwtGeneratorService.CreateToken(user, true);

            return token;
        }
    }
}
