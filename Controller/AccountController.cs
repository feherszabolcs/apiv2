using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.dto.Account;
using apiv2.Models;
using apiv2.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> manager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = manager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto logindto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == logindto.UserName && x.AssociationId == logindto.AssociationId);
            if (user == null)
                return Unauthorized("Invalid username, password or you selected the wrong association!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, logindto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid username or password");

            return Ok(
                new NewUserDto()
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new AppUser()
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    Name = registerDto.Name!,
                    Address = registerDto.Address!,
                    GuardNumber = registerDto.GuardNumber!,
                };

                var createUser = await _userManager.CreateAsync(user, registerDto.Password!);
                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                        return Ok(
                            new NewUserDto()
                            {
                                UserName = user.UserName!,
                                Email = user.Email!,
                                Token = _tokenService.CreateToken(user)
                            }
                        );
                    else
                        return BadRequest(roleResult.Errors);
                }
                else
                {
                    return BadRequest(createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException?.Message ?? e.Message);
                throw;
            }
        }
    }
}