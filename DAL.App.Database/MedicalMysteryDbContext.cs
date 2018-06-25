using DAL.App.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.App.Database
{

    /// <summary>
    /// This is the way to communicate with the database.
    /// </summary>
    public class MedicalMysteryDbContext : DbContext
    {
        public MedicalMysteryDbContext(DbContextOptions<MedicalMysteryDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Following are the tables in the database.
        /// </summary>
        #region Tables
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<SymptomsInDiseases> SymptomsInDiseases { get; set; }
        #endregion

        /// <summary>
        /// Here we show the relations in the database
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder which we use to configure the relations</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Show that SymptomsInDiseases has two foreign keys one for symptoms and other is diseases
            modelBuilder.Entity<SymptomsInDiseases>().HasKey(K => new { K.SymptomId, K.DiseaseId });

            // This shows to the database that SymptomsInDiseases has one Symptom, which has many SymptomsInDiseases
            // and that the SymptomsInDiseases has foreign key SymptomId
            modelBuilder.Entity<SymptomsInDiseases>()
                .HasOne(o => o.Symptom)
                .WithMany(m => m.SymptomsInDiseases)
                .HasForeignKey(fk => fk.SymptomId);

            // This shows to the database that SymptomsInDiseases has one Disease, which has many SymptomsInDiseases
            // and that the SymptomsInDiseases has foreign key DiseaseId
            modelBuilder.Entity<SymptomsInDiseases>()
                .HasOne(o => o.Disease)
                .WithMany(m => m.SymptomsInDiseases)
                .HasForeignKey(fk => fk.DiseaseId);

            // Telling the builder that SymptomsInDiseases ID is generated when adding to the database.
            modelBuilder.Entity<SymptomsInDiseases>()
                .Property(x => x.SymptomsInDiseasesId).ValueGeneratedOnAdd();
            // Telling the builder that Symptom ID is generated when adding to the database.
            modelBuilder.Entity<Symptom>()
                .Property(x => x.SymptomId).ValueGeneratedOnAdd();
            // Telling the builder that Disease ID is generated when adding to the database.
            modelBuilder.Entity<Disease>()
                .Property(x => x.DiseaseId).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
