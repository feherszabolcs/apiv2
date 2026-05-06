using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Models;

namespace apiv2.Service
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}