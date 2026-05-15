using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.dto.Account;
using apiv2.Interfaces;
using apiv2.Mappers;
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
        private readonly string ADMIN_URL = "http://localhost:4200";
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly PlanderDBContext _context;
        private readonly IAssociationRepository _associationRepository;

        public AccountController(UserManager<AppUser> manager, ITokenService tokenService, SignInManager<AppUser> signInManager,
         IEmailService emailService, PlanderDBContext context, IAssociationRepository associationRepository)
        {
            _userManager = manager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailService = emailService;
            _context = context;
            _associationRepository = associationRepository;
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
                UserMapper.GetNewUserDto(user, _tokenService.CreateToken(user))
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = UserMapper.GetAppUserFromRegisterDto(registerDto);
                var createUser = await _userManager.CreateAsync(user, registerDto.Password!);
                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    // var Association = _context.Associations.FirstOrDefault(x => x.Id == registerDto.AssociationId);
                    var association = await _associationRepository.GetByIdAsync(registerDto.AssociationId);
                    if (roleResult.Succeeded)
                    {
                        await _emailService.SendRegisterTemplateAsync(
                            toEmail: association!.Email,
                            fullName: user.Name,
                            associationName: association.Name,
                            guardNumber: user.GuardNumber,
                            confirmUrl: $"{ADMIN_URL}/confirm-register/{user.Id}"
                        );
                        return Ok(
                            UserMapper.GetNewUserDto(user, _tokenService.CreateToken(user))
                        );
                    }
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Confirm([FromRoute] string id, [FromBody] ConfirmUserDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound();

            user.IsConfirmed = dto.IsConfirmed;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound();
            return Ok(UserMapper.GetUserDto(user));
        }
    }
}