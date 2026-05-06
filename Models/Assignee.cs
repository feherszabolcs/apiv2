using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Models
{
    [Table("Assignees")]
    public class Assignee
    {
        public string AppUserId { get; set; } = string.Empty;
        public int AssignmentId { get; set; }

        public AppUser AppUser { get; set; } = null!;
        public Assignment Assignment { get; set; } = null!;
    }
}