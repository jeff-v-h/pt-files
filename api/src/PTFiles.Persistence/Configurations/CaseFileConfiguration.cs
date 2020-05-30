using PTFiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PTFiles.Persistence.Configurations
{
    public class CasefileConfiguration : IEntityTypeConfiguration<Casefile>
    {
        public void Configure(EntityTypeBuilder<Casefile> entity)
        {
            entity.ToTable("Casefiles");

            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.PatientId).IsRequired();

            entity.Property(e => e.Name)
                .IsRequired();

            entity.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("date");

            // requires Patient navigation property in Casefile class
            entity.HasOne(p => p.Patient)
                .WithMany(e => e.Casefiles)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // doesnt require navigation property in Casefile class
            //entity.HasOne<Patient>()
            //    .WithMany()
            //    .HasForeignKey(p => p.PatientId)
            //    .OnDelete(DeleteBehavior.Cascade);

            // When referencing other way around (down to Consultation rather than up from consultation)
            //entity.HasMany(p => p.Consultations)
            //    .WithOne()
            //    .HasForeignKey(p => p.CasefileId);
        }
    }
}
