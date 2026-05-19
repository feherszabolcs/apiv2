using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.dto.Account
{
    public class NewUserDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        [Required]
        public required string Token { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string GuardNumber { get; set; } = string.Empty;
        [Required]
        public bool IsConfirmed { get; set; } = false;
        [Required]
        public required Models.Association Association { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}