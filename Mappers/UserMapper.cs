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
    }
}