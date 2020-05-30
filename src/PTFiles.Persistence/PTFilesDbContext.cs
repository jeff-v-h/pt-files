using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PTFiles.Persistence
{
    public class PTFilesDbContext : DbContext, IPTFilesDbContext
    {
        public PTFilesDbContext(DbContextOptions options) : base(options)
        {
        }

        #region - - - - - - - DbSets - - - - - - -

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Casefile> Casefiles { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<SubjectiveAssessment> SubjectiveAssessments { get; set; }
        public DbSet<ObjectiveAssessment> ObjectiveAssessments { get; set; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
