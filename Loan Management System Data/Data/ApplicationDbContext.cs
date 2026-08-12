using Loan_Management_System_Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System_Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =========================
        // CUSTOMER MANAGEMENT
        // =========================

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<EmploymentDetail> EmploymentDetails { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Nominee> Nominees { get; set; }

        public DbSet<KycDocument> KycDocuments { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }

        public DbSet<Branch> Branches { get; set; }


        // =========================
        // MODEL CONFIGURATION
        // =========================

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Identity configuration
            base.OnModelCreating(builder);

            // Automatically load all configurations
            // from Configurations folder
            builder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}