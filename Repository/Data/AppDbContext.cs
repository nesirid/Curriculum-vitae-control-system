using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CandidateCompany> CandidateCompanies { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Experience> Experiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>()
            .HasMany(c => c.CandidateCompanies)
            .WithOne(cc => cc.Candidate)
            .HasForeignKey(cc => cc.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidateCompany>()
            .HasMany(cc => cc.Positions)
            .WithOne(p => p.CandidateCompany)
            .HasForeignKey(p => p.CandidateCompanyId)
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
        }
    }
}
