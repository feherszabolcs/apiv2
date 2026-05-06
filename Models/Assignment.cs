using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int? AssociationId { get; set; }
        public Association? Association { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow;

        public int? ReportId { get; set; }
        public Report? Report { get; set; }

        public List<Assignee> Assignees { get; set; } = new();

    }
}