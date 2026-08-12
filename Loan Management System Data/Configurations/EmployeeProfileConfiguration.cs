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
    public class EmployeeProfileConfiguration: IEntityTypeConfiguration<EmployeeProfile>
    {
        public void Configure(
           EntityTypeBuilder<EmployeeProfile> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(e => e.Gender)
                .HasMaxLength(10);

            builder.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Designation)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Department)
                .HasMaxLength(100);

            builder.Property(e => e.Salary)
                .HasPrecision(18, 2);

            // Employee → ApplicationUser
            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<EmployeeProfile>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Employee → Branch
            builder.HasOne(e => e.Branch)
                .WithMany(b => b.Employees)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            // EmployeeCode unique
            builder.HasIndex(e => e.EmployeeCode)
                .IsUnique();
        }
    }
}
