using Liga.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.DataAccess.Configurations
{
    public class RefereeConfiguration : IEntityTypeConfiguration<Referee>
    {
        public void Configure(EntityTypeBuilder<Referee> builder)
        {
            builder.Property(r => r.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(r => r.LastName).HasMaxLength(50).IsRequired();


            builder.HasMany(r => r.RefereeLeagues)
                .WithOne(rl => rl.Referee)
                .HasForeignKey(rl => rl.RefereeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(r => r.FirstName).HasMaxLength(50).IsRequired();

        }
    }
}
