using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace apiv2.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string GuardNumber { get; set; } = string.Empty;

        public int? AssociationId { get; set; }
        public Association? Association { get; set; }
        public List<Assignee> Assigned { get; set; } = new List<Assignee>();
    }
}