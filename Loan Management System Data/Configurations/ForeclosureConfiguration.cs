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
    public class ForeclosureConfiguration: IEntityTypeConfiguration<Foreclosure>
    {
        public void Configure(
           EntityTypeBuilder<Foreclosure> builder)
        {
            builder.HasKey(x => x.ForeclosureId);

            builder.Property(x => x.OutstandingPrincipal)
                .HasPrecision(18, 2);

            builder.Property(x => x.InterestAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.PenaltyAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.ForeclosureCharges)
                .HasPrecision(18, 2);

            builder.Property(x => x.TotalAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Reason)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            // Foreclosure -> LoanApplication
            builder.HasOne(x => x.LoanApplication)
                .WithMany()
                .HasForeignKey(x => x.LoanApplicationId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
