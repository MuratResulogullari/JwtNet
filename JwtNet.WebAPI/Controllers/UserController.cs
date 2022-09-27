using JwtNet.WebAPI.Business;
using JwtNet.WebAPI.Business.Abstract;
using JwtNet.WebAPI.Business.CurrentUser;
using JwtNet.WebAPI.Models.Dtos;
using JwtNet.WebAPI.Models.Entities;
using JwtNet.WebAPI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
        private readonly ICurrentUser _currentUser;
        public UserController(IConfiguration configuration,
            IUserService userService,
            IRoleService roleService,
            IRefreshTokenService refreshTokenService,
            ICurrentUser currentUser
            )
        {
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
            _refreshTokenService = refreshTokenService;
            _currentUser = currentUser;
        }
        /// <summary>
        /// Get User infromation on claims When user was login
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("GetUser")]
        public ActionResult<string> GetUser()
        {
            var userName = _currentUser.GetUserName();
            return Ok(userName);
        }
        /// <summary>
        /// User Login Progress For Authoriz and Authentication
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginViewModel loginViewModel)
        {
            var requestResult = new ResultViewModel();
            var result = _userService.GetByUserName(loginViewModel.UserName);
            if (!result.IsSuccess)
            {
                requestResult.IsSuccess = false;
                requestResult.Result = result;
                requestResult.Message = "User not found !";
            }
            else
            {
                User user = (User)result.Result;
                if (!VerifyPasswordHash(loginViewModel.Password, user.PasswordHash, user.PasswordSalt))
                {
                    requestResult.IsSuccess = false;
                    requestResult.Result = result;
                    requestResult.Message = "Wrong password !";
                }
                else
                {
                    var newRefreshToken = GenerateRefreshToken();
                    newRefreshToken.Token = CreateToken(user);
                    SetRefreshToken(newRefreshToken, user);

                    requestResult.IsSuccess = true;
                    requestResult.Result = newRefreshToken.Token;
                    requestResult.Message = "Login Successfull.";
                }
            }
            return Ok(requestResult);


        }
        /// <summary>
        /// Register new user on database
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<ActionResult<ResultViewModel>> Register(UserDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User
            {
                UserName = userDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1,
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            ResultViewModel result = _userService.Create(newUser);
            if (result.IsSuccess)
            {
                var  resultUser = _userService.GetByUserName(userDto.UserName);
                var user = (User)resultUser.Result;

                var refreshToken = new RefreshToken
                {
                    UserId=user.Id,
                    Token= CreateToken(user),
                    ExpiryDate = DateTime.Now,
                    CreatedOn =DateTime.Now,
                    IsActive=true,
                    TokenType=1
                };
                result = _refreshTokenService.Create(refreshToken);
                
            }
            return Ok(result);
        }
        /// <summary>
        /// If you send old token, this  get you new token 
        /// same time refresh  token on database and cookies
        /// </summary>
        /// <param name="tokenResponse"></param>
        /// <returns></returns>
        [HttpPost("refreshtoken")]
        public async Task<ActionResult<string>> RefreshToken(TokenResponse tokenResponse)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = _refreshTokenService.GetByToken(tokenResponse.RefreshToken);
            if (!result.IsSuccess)
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            RefreshToken token = (RefreshToken)result.Result;
            var user = _userService.GetById(token.UserId);
            if (user == null)
                return Unauthorized("Invalid Refresh Token.");
            
            var newRefreshToken = GenerateRefreshToken();
            newRefreshToken.Token = CreateToken(user);
            SetRefreshToken(newRefreshToken,user);
            return Ok(newRefreshToken.Token);
        }
        /// <summary>
        ///  create json web token and return jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
        
        /// <summary>
        /// create new token with  random token for dont not result  null
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// token add database and cookies
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="user"></param>
        private void SetRefreshToken(RefreshToken refreshToken,User user)
        {
                
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiryDate
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            var result = _refreshTokenService.GetByUserId(user.Id);
            if (result.IsSuccess)
            {
                RefreshToken newRefreshToken = (RefreshToken)result.Result;
                newRefreshToken.Token = refreshToken.Token;
                newRefreshToken.CreatedOn = refreshToken.CreatedOn;
                newRefreshToken.ExpiryDate = refreshToken.ExpiryDate;
                newRefreshToken.IsActive = true;
                 _refreshTokenService.Update(newRefreshToken);
            }
        }
        /// <summary>
        /// create passwordhash byte types
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;//hmac.Key=128 characters
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));//hmac.Key=68 characters
            }
        }
        /// <summary>
        /// Password controller there for true or false
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
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
