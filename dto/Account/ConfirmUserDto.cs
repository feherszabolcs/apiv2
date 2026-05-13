using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.dto.Account
{
    public class ConfirmUserDto
    {
        [Required]
        public bool IsConfirmed { get; set; }

    }
}