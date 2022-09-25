using JwtNet.WebAPI.Business;
using JwtNet.WebAPI.Business.Abstract;
using JwtNet.WebAPI.Models.Dtos;
using JwtNet.WebAPI.Models.Entities;
using JwtNet.WebAPI.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtNet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IRefreshTokenService _refreshTokenService;

        
        public UserController(IConfiguration configuration,
            IUserService userService,
            IRoleService roleService,
            IRefreshTokenService refreshTokenService)
        {
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
            _refreshTokenService = refreshTokenService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginViewModel loginViewModel)
        {
            var result = _userService.GetByUserName(loginViewModel.UserName);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            else
            {
                User user = (User)result.Result;
                if (!VerifyPasswordHash(loginViewModel.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }
                else
                {
                    string token = CreateToken(user);

                    var refreshToken = GenerateRefreshToken();
                    SetRefreshToken(refreshToken,user);

                    return Ok(token);
                }
            }
           
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ResultViewModel>> Register(UserDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                UserName = userDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1,
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            ResultViewModel result = _userService.Create(user);
            return Ok(result);
        }
        private string CreateToken(User user)
        {
            var role = _roleService.GetById(user.RoleId);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role.RoleName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiryDate = DateTime.Now.AddDays(7),
                CreatedOn = DateTime.Now
            };

            return refreshToken;
        }
        private void SetRefreshToken(RefreshToken newRefreshToken,User user)
        {
                
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpiryDate
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            var result = _refreshTokenService.GetByUserId(user.Id);
            if (result.IsSuccess)
            {
                RefreshToken refreshToken = (RefreshToken)result.Result;
                refreshToken.Token = newRefreshToken.Token;
                refreshToken.CreatedOn = newRefreshToken.CreatedOn;
                refreshToken.ExpiryDate = newRefreshToken.ExpiryDate;
                refreshToken.IsActive = true;
                 _refreshTokenService.Update(refreshToken);
            }
             
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
