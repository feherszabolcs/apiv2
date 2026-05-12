using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.dto.Association
{
    public class AssociationPatchDto
    {
        [Required]
        public bool IsConfirmed { get; set; }
    }
}