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
    public class PenaltyConfiguration: IEntityTypeConfiguration<Penalty>
    {
        public void Configure(
            EntityTypeBuilder<Penalty> builder)
        {
            builder.HasKey(x => x.PenaltyId);

            builder.Property(x => x.PenaltyAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Reason)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            // Penalty -> LoanApplication
            builder.HasOne(x => x.LoanApplication)
                .WithMany()
                .HasForeignKey(x => x.LoanApplicationId)
                .OnDelete(DeleteBehavior.NoAction);

            // Penalty -> LoanRepayment
            builder.HasOne(x => x.LoanRepayment)
                .WithMany()
                .HasForeignKey(x => x.LoanRepaymentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
