using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.dto.Account;
using apiv2.Models;

namespace apiv2.Mappers
{
    public static class UserMapper
    {
        public static NewUserDto GetNewUserDto(this AppUser appUser, string token)
        {
            return new NewUserDto()
            {
                UserName = appUser.UserName!,
                Email = appUser.Email!,
                Token = token
            };
        }
        public static AppUser GetAppUserFromRegisterDto(this RegisterDto registerDto)
        {
            return new AppUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Name = registerDto.Name!,
                Address = registerDto.Address!,
                GuardNumber = registerDto.GuardNumber!,
                AssociationId = registerDto.AssociationId,
                IsConfirmed = false
            };
        }
        public static UserDto GetUserDto(this AppUser appUser, List<string> roles)
        {
            return new UserDto()
            {
                Name = appUser.Name,
                Email = appUser.Email!,
                Address = appUser.Address,
                GuardNumber = appUser.GuardNumber,
                IsConfirmed = appUser.IsConfirmed,
                AssociationName = appUser.Association != null ? appUser.Association.Name : string.Empty,
                Roles = roles
            };
        }
    }
}