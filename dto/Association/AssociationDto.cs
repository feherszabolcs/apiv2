using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.dto.Association
{
    public class AssociationDto
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Location { get; set; } = "";
        [Required]
        public string Certificate { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
    }
}