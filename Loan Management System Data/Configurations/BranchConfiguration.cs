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
    public class BranchConfiguration: IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.BranchId);

            builder.Property(b => b.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.PinCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(b => b.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(b => b.Email)
                .HasMaxLength(100);

            builder.Property(b => b.IsActive)
                .HasDefaultValue(true);

            // Branch → Employees
            builder.HasMany(b => b.Employees)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.SetNull);
        }


    }
}
