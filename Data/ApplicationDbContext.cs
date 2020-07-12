using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stajbul.Models;

namespace stajbul.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<stajbul.Models.jobPosting> jobPosting { get; set; }
        public DbSet<stajbul.Models.talents> talents { get; set; }
        public DbSet<stajbul.Models.talentsofJobs> talentsofJobs { get; set; }
        public DbSet<stajbul.Models.stajbulUser> stajbulUser { get; set; }
        public DbSet<stajbul.Models.jobApplications> jobApplications { get; set; }
    }
}
