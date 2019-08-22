using Liga.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.DataAccess.Configurations
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();

            builder.HasOne(c => c.City)
                .WithMany(cl => cl.Clubs)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.League)
                .WithMany(l => l.Clubs)
                .HasForeignKey(c => c.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Players)
                .WithOne(p => p.Club)
                .HasForeignKey(p => p.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
