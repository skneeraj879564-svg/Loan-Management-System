using Loan_Management_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Loan_Management_System_Data.Configurations
{
    public class AddressConfiguration: IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // Primary Key
            builder.HasKey(x => x.AddressId);

            // Address Line
            builder.Property(x => x.AddressLine)
                .IsRequired()
                .HasMaxLength(200);

            // City
            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            // State
            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(100);

            // Pin Code
            builder.Property(x => x.PinCode)
                .IsRequired()
                .HasMaxLength(10);

            // Country
            builder.Property(x => x.Country)
                .HasMaxLength(100);

            // Address Type
            builder.Property(x => x.AddressType)
                .HasMaxLength(20);

            // Customer → Address (1 : 1)
            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Address)
                .HasForeignKey<Address>(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
