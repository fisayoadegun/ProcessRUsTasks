using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProcessRUsTasks.Dtos;
using ProcessRUsTasks.Exceptions;
using ProcessRUsTasks.Helpers;
using ProcessRUsTasks.Models;
using ProcessRUsTasks.Services;
using ProcessRUsTasks.Services.Interfaces;

namespace ProcessRUsTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(IJwtService jwtService, IUserService userService, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            _jwtService = jwtService;
            _userService = userService;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var user = await _userService.GetUserByEmail(dto.Email);

            if (user == null)
            {
                throw new BadRequestException($"Invalid Login Attempt");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, dto.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new BadRequestException($"Invalid Login Attempt");
            }                      

            var userClaims = await _jwtService.GetClaimsAsync(user);
            var accessToken = _jwtService.GenerateJwtAccessToken(userClaims);

            var response = new LoginResponse
            {
                Id = user.Id,
                Token = accessToken,
                Email = user.Email,
                Roles = await _userService.GetUserRoles(user),                
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<ApplicationUser>(dto);
            user.UserName = $"{user.FirstName}.{user.LastName}";
            await _userService.CreateUser(user, dto.Password);

            await _userService.AddUserRoleAsync(user.Id, RoleTypes.BackOffice);

            return Ok();
        }
    }
}
