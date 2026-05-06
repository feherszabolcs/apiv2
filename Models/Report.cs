using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Method { get; set; } = string.Empty;
        public string[] ImageUrls { get; set; } = [];
        public string Purpose { get; set; } = string.Empty;
        
    }
}