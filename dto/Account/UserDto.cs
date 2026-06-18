using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.dto.Account
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string GuardNumber { get; set; } = string.Empty;
        [Required]
        public bool IsConfirmed { get; set; } = false;
        public Models.Association Association { get; set; } = new();
        public List<string> Roles { get; set; } = new List<string>();

    }
}