using Loan_Management_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Loan_Management_System_Data.Configurations
{
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(x => x.Gender)
                .HasMaxLength(10);

            builder.Property(x => x.CustomerCode)
                .HasMaxLength(20);

            builder.Property(x => x.PanNumber)
                .HasMaxLength(20);

            builder.Property(x => x.AadhaarNumber)
                .HasMaxLength(20);

            builder.HasIndex(x => x.CustomerCode)
                .IsUnique();

            builder.HasIndex(x => x.PanNumber)
                .IsUnique();

            builder.HasIndex(x => x.AadhaarNumber)
                .IsUnique();
        }
    }
}
