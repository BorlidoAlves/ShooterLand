using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shooterlandWebBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using shooterlandWebBack.Helpers;
using shooterlandWebBack.Entity;
using shooterlandWebBack.Services.UserService;
using AutoMapper;
using shooterlandWebBack.Models.Users;
using shooterlandWebBack.Services;

namespace shooterlandWebBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly AppSettings _appSettings;
        private IUserService _userService;
        private IEmailService _emailService;
        private IGameStatsService _gameStatsService;
        private IUsersAchievService _usersAchievService;
        private IMapper _mapper;

        public UserController(
            IUserService userService,
            IEmailService emailService,
            IGameStatsService gameStatsService,
            IUsersAchievService usersAchievService,
            IMapper mapper,
            IOptions<AppSettings> appSettings) 
        {
            _emailService = emailService;
            _userService = userService;
            _gameStatsService = gameStatsService;
            _usersAchievService = usersAchievService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
                 
        }

        /// <summary>
        /// Does the authentication of a User 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody]AuthenticateModel userModel)
        {
            
            var user = _userService.Authenticate(userModel.Username, userModel.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Username or Password invalid" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Type.ToString())
                }),
                
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                id = user.Id,
                username = user.Username,
                token = tokenString,
                type = user.Type
            });
        }

        /// <summary>
        /// Creates a new User account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            var user = _mapper.Map<User>(model);

            try
            {
                
                _userService.Create(user, model.Password);
                _emailService.RegistrationEmail(model.Username, model.Password, model.Email);
                return Ok(new { Message = "Sucesfull Registation"});
            }
            catch (AppException ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Used for replace the account password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword([FromBody]ForgetCredencialModel model)
        {
            try
            {
                var newPassword = _userService.GenerateRandomPassword();
                var user = _userService.UpdateCredentials(model.Email, newPassword);
                _emailService.ForgetCredencialsEmail(user.Username, newPassword, model.Email);

                return Ok(new { Message = "New Password in the email !" });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// User information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/Information/{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if(user != null){
                var model = _mapper.Map<UserModel>(user);
                return Ok(model);
            }
            else 
            {
                return BadRequest(new { message = "User Id does not exist" });
            }
            
        }

        /// <summary>
        /// Returns the User Stats
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("Stats/{id}")]
        public IActionResult StatsById(int id)
        {
            var stast = _gameStatsService.UserGameStats(id);
            return Ok(stast);
        }

        /// <summary>
        /// Get Achivements of a given id user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("UserAchievements/{id}")]
        public IActionResult UserAchivements(int id)
        {
            var achiev = _usersAchievService.UserAchievments(id);
            return Ok(achiev);
        }
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Entity.Type.Admin)]
        [HttpGet("AllUsers")]
        public IActionResult AllUsers()
        {
            var userList = _userService.GetAll();
            var model = _mapper.Map<UserModel[]>(userList);
            return Ok(model);
        }

        /// <summary>
        /// Used to update the account password
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("UpdatePassword/{id}")]
        public IActionResult UpdatePassword([FromBody]UpdatePassword model, int id)
        {
            try
            {
                _userService.UpdatePassword(model.Password, id);
                return Ok(new { Message = "Password Updated"});
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

       /// <summary>
       /// Used to delete the account of the given id user 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpDelete("RemoveUser/{id}")]
        public IActionResult RemoveUser(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok(new { Message = "User removed" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
