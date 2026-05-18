using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Models;
using Microsoft.AspNetCore.Identity;

namespace apiv2.Data
{
    public class AssociationScopedValidator : IUserValidator<AppUser>
    {
        private readonly PlanderDBContext _context;
        public AssociationScopedValidator(PlanderDBContext context)
        {
            _context = context;
        }
        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {

            if (String.IsNullOrWhiteSpace(user.UserName) || user.AssociationId == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidUser",
                    Description = "A felhasználónév és a szervezet megadása kötelező!"
                });
            }

            var isDupe = _context.Users.Any(x =>
            x.UserName == user.UserName &&
            x.AssociationId == user.AssociationId &&
            x.Id != user.Id
            );



            if (isDupe)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateUserName",
                    Description = $"A megadott ({user.UserName}) felhasználónevet már valaki használja ebben a szervezetben!"
                });
            }
            return IdentityResult.Success;
        }
    }
}