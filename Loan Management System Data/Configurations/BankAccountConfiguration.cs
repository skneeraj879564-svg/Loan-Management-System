using Loan_Management_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Configurations
{
    public class BankAccountConfiguration: IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(
          EntityTypeBuilder<BankAccount> builder)
        {
            // Primary Key
            builder.HasKey(x => x.BankAccountId);

            // Bank Name
            builder.Property(x => x.BankName)
                .IsRequired()
                .HasMaxLength(150);

            // Account Holder Name
            builder.Property(x => x.AccountHolderName)
                .IsRequired()
                .HasMaxLength(100);

            // Account Number
            builder.Property(x => x.AccountNumber)
                .IsRequired()
                .HasMaxLength(30);

            // IFSC Code
            builder.Property(x => x.IFSCCode)
                .IsRequired()
                .HasMaxLength(20);

            // Account Type
            builder.Property(x => x.AccountType)
                .HasMaxLength(50);

            // Branch Name
            builder.Property(x => x.BranchName)
                .HasMaxLength(100);

            // Customer → BankAccount (1 : 1)
            builder.HasOne(x => x.Customer)
                .WithOne(x => x.BankAccount)
                .HasForeignKey<BankAccount>(
                    x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // One customer should have only one primary bank account
            builder.HasIndex(x => x.CustomerId)
                .IsUnique();
        }
    }
}
