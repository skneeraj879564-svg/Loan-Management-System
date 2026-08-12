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
    public class EmploymentDetailConfiguration : IEntityTypeConfiguration<EmploymentDetail>
    {
        public void Configure( EntityTypeBuilder<EmploymentDetail> builder)
        {
            // Primary Key
            builder.HasKey(x => x.EmploymentDetailId);

            // Employment Type
            builder.Property(x => x.EmploymentType)
                .IsRequired()
                .HasMaxLength(100);

            // Company Name
            builder.Property(x => x.CompanyName)
                .HasMaxLength(150);

            // Designation
            builder.Property(x => x.Designation)
                .HasMaxLength(100);

            // Monthly Income
            builder.Property(x => x.MonthlyIncome)
                .HasPrecision(18, 2);

            // Annual Income
            builder.Property(x => x.AnnualIncome)
                .HasPrecision(18, 2);

            // Company Address
            builder.Property(x => x.CompanyAddress)
                .HasMaxLength(200);

            // Office Phone
            builder.Property(x => x.OfficePhoneNumber)
                .HasMaxLength(20);

            // Customer → EmploymentDetail (1 : 1)
            builder.HasOne(x => x.Customer)
                .WithOne(x => x.EmploymentDetail)
                .HasForeignKey<EmploymentDetail>(
                    x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
