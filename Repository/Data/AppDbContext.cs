using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<CandidateCompany> CandidateCompanies { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<CandidatePhoto> CandidatePhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>()
            .HasMany(c => c.CandidateCompanies)
            .WithOne(cc => cc.Candidate)
            .HasForeignKey(cc => cc.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidateCompany>()
            .HasOne(cc => cc.Candidate)
            .WithMany(c => c.CandidateCompanies)
            .HasForeignKey(cc => cc.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidateCompany>()
            .HasOne(cc => cc.Company)
            .WithMany()
            .HasForeignKey(cc => cc.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Experience>()
            .HasOne<Candidate>()
            .WithMany(c => c.WorkHistory)
            .HasForeignKey(e => e.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhoneNumber>()
            .HasOne(p => p.Candidate)
            .WithMany(c => c.PhoneNumbers)
            .HasForeignKey(p => p.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Position>()
            .HasOne(p => p.CandidateCompany)
            .WithMany(cc => cc.Positions)
            .HasForeignKey(p => p.CandidateCompanyId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidatePhoto>()
            .HasOne(p => p.Candidate)
            .WithMany(c => c.Photos)
            .HasForeignKey(p => p.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
