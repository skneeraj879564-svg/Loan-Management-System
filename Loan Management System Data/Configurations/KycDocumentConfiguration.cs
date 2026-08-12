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
    public class KycDocumentConfiguration: IEntityTypeConfiguration<KycDocument>
    {
        public void Configure(
           EntityTypeBuilder<KycDocument> builder)
        {
            // Primary Key
            builder.HasKey(x => x.KycDocumentId);

            // Document Type
            builder.Property(x => x.DocumentType)
                .IsRequired()
                .HasMaxLength(50);

            // Document Number
            builder.Property(x => x.DocumentNumber)
                .IsRequired()
                .HasMaxLength(100);

            // Document File Path
            builder.Property(x => x.DocumentFilePath)
                .IsRequired()
                .HasMaxLength(500);

            // Verification Status
            builder.Property(x => x.VerificationStatus)
                .HasMaxLength(20);

            // Remarks
            builder.Property(x => x.Remarks)
                .HasMaxLength(500);

            // Customer → KYC Documents (1 : Many)
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.KycDocuments)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
