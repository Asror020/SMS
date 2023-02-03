using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMS.BLL.Models.AuthenticationModels;
using SMS.BLL.Models.Configurations;
using SMS.BLL.Services.AuthenticationServices.Interfaces;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SMS.BLL.Services.AuthenticationServices
{
    public class JWTGeneratorService : IJWTGeneratorService
    {
        private readonly AuthenticationConfigurations _jwtConfiguration;
        private readonly IUserService _userService;

        public JWTGeneratorService(IOptions<AuthenticationConfigurations> jwtConfiguration, IUserService userService)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _userService = userService;
        }
        public async Task<TokenDto> CreateToken(User user, bool populateExp)
        {
            if (user == null) throw new ArgumentException();

            var identity = GetIdentity(user);

            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: identity.Claims,
                expires: DateTime.UtcNow.AddHours(_jwtConfiguration.Lifetime),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)

            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            if(populateExp) user.RefreshTokenExpiryDate = DateTime.UtcNow;

            await _userService.UpdateAsync(user.Id, user);

             return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Principal = new ClaimsPrincipal(identity)
            };
        }

        public ClaimsIdentity GetIdentity(User user)
        {
            if(user == null) throw new ArgumentException(); 

            if(string.IsNullOrWhiteSpace(user.EmailAddress)) throw new ArgumentException();

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.EmailAddress),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "JwtAuth", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return identity;
        }
        
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rgn = RandomNumberGenerator.Create())
            {
                rgn.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
