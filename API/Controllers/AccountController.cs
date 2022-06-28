using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager,
            TokenService tokenService, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDTO>> Login(LoginDTO input)
        {
            if (input.Email == null) return BadRequest("Please provide an email");
            if (input.Password == null) return BadRequest("Please provide a password");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == input.Email.ToUpper());

            if (user == null) return Unauthorized("No account found with this email/password combination");

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

            if (!result.Succeeded) return Unauthorized("No account found with this email/password combination");

            var userResult = _mapper.Map<AppUserDTO>(user);
            userResult.Token = await _tokenService.CreateToken(user);

            return userResult;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserDTO>> Register(RegisterDTO input)
        {
            if (await _userManager.EmailTaken(input.Email)) return BadRequest("Email is taken");

            var user = _mapper.Map<AppUser>(input);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            var returnUser = _mapper.Map<AppUserDTO>(user);
            returnUser.Token = await _tokenService.CreateToken(user);

            return returnUser;
        }
    }
}