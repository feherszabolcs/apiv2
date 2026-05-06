using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Models;

namespace apiv2.dto.Account
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public int AssociationId { get; set; }

    }
}