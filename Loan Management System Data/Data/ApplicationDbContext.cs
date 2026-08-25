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


        // =========================
        // EMPLOYEE MANAGEMENT
        // =========================

        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<Branch> Branches { get; set; }


        // =========================
        // LOAN MANAGEMENT
        // =========================

        public DbSet<LoanProduct> LoanProducts { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Foreclosure> Foreclosures { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<LoanDocument> LoanDocuments { get; set; }
        public DbSet<VerificationHistory> VerificationHistories { get; set; }
        public DbSet<Loan> Loans { get; set; }


        // =========================
        // MODEL CONFIGURATION
        // =========================

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // =========================
            // IDENTITY CONFIGURATION
            // =========================

            base.OnModelCreating(builder);


            // =========================
            // LOAN PRODUCT DECIMAL
            // =========================

            builder.Entity<LoanProduct>().Property(x => x.MinimumAmount)
                .HasPrecision(18, 2);

            builder.Entity<LoanProduct>()
                .Property(x => x.MaximumAmount)
                .HasPrecision(18, 2);

            builder.Entity<LoanProduct>()
                .Property(x => x.InterestRate)
                .HasPrecision(5, 2);


            // =========================
            // LOAN APPLICATION DECIMAL
            // =========================

            builder.Entity<LoanApplication>()
                .Property(x => x.RequestedAmount)
                .HasPrecision(18, 2);


            // =========================
            // LOAN DECIMAL
            // =========================

            builder.Entity<Loan>()
                .Property(x => x.ApprovedAmount)
                .HasPrecision(18, 2);

            builder.Entity<Loan>()
                .Property(x => x.InterestRate)
                .HasPrecision(5, 2);

            builder.Entity<Loan>()
                .Property(x => x.ProcessingFee)
                .HasPrecision(18, 2);

            builder.Entity<Loan>()
                .Property(x => x.OutstandingAmount)
                .HasPrecision(18, 2);


            // =========================
            // VERIFICATION HISTORY
            // =========================
            // Prevent SQL Server multiple cascade paths

            builder.Entity<VerificationHistory>()
                .HasOne(x => x.LoanApplication)
                .WithMany(x => x.VerificationHistories)
                .HasForeignKey(x => x.LoanApplicationId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // AUTOMATIC CONFIGURATIONS
            // =========================

            builder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}