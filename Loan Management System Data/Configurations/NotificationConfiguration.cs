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
    public class NotificationConfiguration: IEntityTypeConfiguration<Notification>
    {
        public void Configure(
           EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.NotificationId);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.IsRead)
                .HasDefaultValue(false);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.HasIndex(x => x.UserId);
        }

    }
}
