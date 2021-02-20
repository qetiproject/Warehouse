using ApplicationShared.DTOs;
using ApplicationShared.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Warehouse.CoreServices.Interfaces;
using Warehouse.DatabaseEntity.DB;
using Warehouse.DomainModels.Models;

namespace ApplicationShared.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;
        public UserService(
            IUserRepository userRepo,
            ITokenService tokenService,
            ApplicationDbContext context,
            IMapper mapper
        )
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }
        public async Task<Result> GetUser(int id)
        {
            Result result = new Result();

            User userById = await _userRepo.GetUser(id);

            try
            {
                if(userById!= null)
                {
                    UserDetailDto user = _mapper.Map<UserDetailDto>(userById);
                    result.Data = user;
                    result.Message = "See User detail";
                    result.ListCount = 1;
                }
                else
                {
                    result.Code = 500;
                    result.Success = false;
                    result.ErrorMessage = "This user doesn't exist";
                }
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Code = ex.HResult;
            }

            return result;
        }

        public async Task<Result> Login(UserLoginDto userLogin)
        {
            Result result = new Result();

            User user = await _userRepo.Login(userLogin.Username, userLogin.Password);

            UserDto userDto = new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Gender = user.Gender,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            try
            {
                result.Data = userDto;
                result.Message = "User logged in";
                result.ListCount = 1;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Code = ex.HResult;
            }

            return result;
        }

        public async Task<Result> Register(UserRegisterDto userRegister)
        {
            Result result = new Result();

            User userToCreate = _mapper.Map<User>(userRegister);

            //User createdUser = await _userRepo.Register(userToCreate, userRegister.Password);
            
            try
            {
                await _context.Users.AddAsync(userToCreate);
                await _context.SaveChangesAsync();
                UserDetailDto user = _mapper.Map<UserDetailDto>(userToCreate);
                result.Data = user;
                result.ListCount = 1;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Code = ex.HResult;
            }

            return result;
        }
    }
}
