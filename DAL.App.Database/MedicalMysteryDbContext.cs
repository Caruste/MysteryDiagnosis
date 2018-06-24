using DAL.App.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.App.Database
{
    public class MedicalMysteryDbContext : DbContext, IDataContext
    {
        public MedicalMysteryDbContext(DbContextOptions<MedicalMysteryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<SymptomsInDiseases> SymptomsInDiseases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SymptomsInDiseases>().HasKey(K => new { K.SymptomId, K.DiseaseId });

            modelBuilder.Entity<SymptomsInDiseases>()
                .HasOne(o => o.Symptom)
                .WithMany(m => m.SymptomsInDiseases)
                .HasForeignKey(fk => fk.SymptomId);

            modelBuilder.Entity<SymptomsInDiseases>()
                .HasOne(o => o.Disease)
                .WithMany(m => m.SymptomsInDiseases)
                .HasForeignKey(fk => fk.DiseaseId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
