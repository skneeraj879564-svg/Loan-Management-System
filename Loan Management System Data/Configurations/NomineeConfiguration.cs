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
    public class NomineeConfiguration: IEntityTypeConfiguration<Nominee>
    {
        public void Configure(
           EntityTypeBuilder<Nominee> builder)
        {
            // Primary Key
            builder.HasKey(x => x.NomineeId);

            // Full Name
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            // Relationship
            builder.Property(x => x.Relationship)
                .IsRequired()
                .HasMaxLength(50);

            // Phone Number
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);

            // Address
            builder.Property(x => x.Address)
                .HasMaxLength(200);

            // Aadhaar Number
            builder.Property(x => x.AadhaarNumber)
                .HasMaxLength(20);

            // Customer → Nominee (1 : 1)
            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Nominee)
                .HasForeignKey<Nominee>(
                    x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // One customer → one nominee
            builder.HasIndex(x => x.CustomerId)
                .IsUnique();
        }
    }
}
