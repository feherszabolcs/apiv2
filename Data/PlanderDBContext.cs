using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Data
{
    public class PlanderDBContext : IdentityDbContext<Models.AppUser>
    {
        public DbSet<Models.Association> Associations { get; set; }
        public DbSet<Models.Assignment> Assignments { get; set; }
        public DbSet<Models.Report> Reports { get; set; }
        // public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Assignee> Assignees { get; set; }

        public PlanderDBContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Assignee>(x =>x.HasKey(p => new { p.AppUserId, p.AssignmentId }));

            builder.Entity<Models.Assignee>()
            .HasOne(a => a.AppUser)
            .WithMany(u => u.Assigned)
            .HasForeignKey(a => a.AppUserId);

            builder.Entity<Models.Assignee>()
            .HasOne(a => a.Assignment)
            .WithMany(u=> u.Assignees)
            .HasForeignKey(a => a.AssignmentId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new() {Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "6eca4832-fd33-4e68-8190-10a9274da274"},
                new() {Id = "2", Name = "Leader", NormalizedName = "LEADER", ConcurrencyStamp = "37635dfc-538e-4edc-aef0-5e0c71606827" },
                new() {Id = "3", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "90637356-bccb-45e7-b8fc-17acb7d39c81"  }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}