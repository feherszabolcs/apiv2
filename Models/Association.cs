using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Models
{
    public class Association
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Certificate { get; set; } = string.Empty;

        // public List<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}