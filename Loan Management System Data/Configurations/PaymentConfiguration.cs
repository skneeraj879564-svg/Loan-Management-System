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
    public class PaymentConfiguration: IEntityTypeConfiguration<Payment>
    {
        public void Configure(
           EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.PaymentId);

            builder.Property(x => x.PaymentAmount)
                .HasPrecision(18, 2);

            // Payment → LoanApplication
            builder.HasOne(x => x.LoanApplication)
                .WithMany()
                .HasForeignKey(x => x.LoanApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment → LoanRepayment
            // Cascade disabled to avoid multiple cascade paths
            builder.HasOne(x => x.LoanRepayment)
                .WithMany()
                .HasForeignKey(x => x.LoanRepaymentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
