using Liga.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.DataAccess.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(50).IsRequired();

            builder.Property(p => p.Email).HasMaxLength(90).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(50).IsRequired();

            builder.HasOne(p => p.City)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Club)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Position)
                .WithMany(po => po.Players)
                .HasForeignKey(po => po.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");


        }
    }
}
