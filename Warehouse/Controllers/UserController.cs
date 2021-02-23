using ApplicationShared.DTOs;
using ApplicationShared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Warehouse.CoreServices.Interfaces;
using Warehouse.DomainModels.Models;

namespace Warehouse.Controllers
{

    public class UserController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepo;

        public UserController(
            ITokenService tokenService,
            IUserService userService,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserRepository userRepo
        )
        {
            _tokenService = tokenService;
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserRegisterDto registerUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userRepo.UserExists(registerUser.Username))
                return BadRequest("Username already exists");

            User user = _mapper.Map<User>(registerUser);

            user.UserName = registerUser.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Administrator");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Email = user.Email
            };
        }

        //[HttpPost("Login")]
        //public async Task<Result> Login(UserLoginDto userLoginDto)
        //{
        //    return await _userService.Login(userLoginDto);
        //}

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == userLoginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender
            };
        }


        [HttpGet("GetUser")]
        public async Task<Result> GetUser(int id)
        {
            return await _userService.GetUser(id);
        }
    }
}
