using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<PatientSchedule> PatientSchedules { get; set; }
        public DbSet<PatientType> PatientTypes { get; set; }
        public DbSet<PatientTypeEnrollment> PatientTypeEnrollments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleEnrollment> RoleEnrollments { get; set; }
        public DbSet<RegConfirmation> RegConfirmations { get; set; }
    }
}
