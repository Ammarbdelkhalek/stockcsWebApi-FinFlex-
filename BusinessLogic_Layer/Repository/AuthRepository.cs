using Domain_or_abstractionLayer.Entites;
using Domain_or_abstractionLayer.helper;
using Domain_or_abstractionLayer.Interface;
using Domain_or_abstractionLayer.modelsDTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Layer.Repository
{

    public class AuthRepository : IAuthRepository
    {
        private UserManager<APPUser> UserManager ;
        private JwtHelper options;

        public AuthRepository(UserManager<APPUser> userManager, IOptions<JwtHelper> options)
        {
            UserManager = userManager;
            this.options = options.Value;
        }

        public async Task<AuthModel> LoginAsync(LoginModel loginModel)
        {
            var User = await UserManager.FindByEmailAsync(loginModel.Email);
            if (User == null || !await UserManager.CheckPasswordAsync(User,loginModel.Password) ) 
            {
                return new AuthModel { Message = " UserName Or Password Invalid"};
            }
            var token = await GenerateToken(User);

            await UserManager.GetRolesAsync(User);
            return
                new AuthModel {
                    Message = " you logged in Sucessfuly ",
                    Email = User.Email,
                    Roles = new List<string> { "User" },
                    Token = token,
                    IsAuthenticated = true,
                ExpirationDate = new JwtSecurityToken().ValidTo,
            };
        }

        public async Task<AuthModel> RegistrationAsync(RegistrationModel registerModel)
        {
            if(await UserManager.FindByEmailAsync(registerModel.Email) is not null)
            {
                return new AuthModel { Message = "Email Is Already Exist" };
                
            }
            if(await UserManager.FindByNameAsync(registerModel.UserName)is not null)
            {
                return new AuthModel { Message = "UserName is already Exist" };
            }

            var user = new APPUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
            };
            var result = await UserManager.CreateAsync(user , registerModel.Password);
            if(!result.Succeeded)
            {
                foreach(var err in result.Errors)
                return new AuthModel { Message = err.Description };
            }
            var token = await GenerateToken(user);
            await UserManager.AddToRoleAsync(user, "User");

            return new AuthModel
            {
                Message = "Registed Sucessfully",
                Email = user.Email,
                Roles = new List<string> { "User"},
                Token = token,
                IsAuthenticated = true,
                ExpirationDate = new JwtSecurityToken ().ValidTo,
            };
        }

        public async Task<string> GenerateToken(APPUser user)
        {

            var userClaims = await UserManager.GetClaimsAsync(user);
            var roles = await UserManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.key));
            var SercurityTokenCredintial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var SecurityToken = new JwtSecurityTokenHandler();
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                issuer: options.Issuer,
                audience: options.Audience,
                expires: DateTime.Now.AddDays(options.LifeTime),
                signingCredentials: SercurityTokenCredintial
                );

            var token = SecurityToken.WriteToken(jwtSecurityToken);
            return token;
        }

    }
}
