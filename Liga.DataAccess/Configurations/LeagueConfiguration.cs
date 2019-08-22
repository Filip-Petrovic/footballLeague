using Liga.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.DataAccess.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.Property(l => l.Name).HasMaxLength(30).IsRequired();
            builder.Property(l => l.Level).HasMaxLength(5).IsRequired();

            builder.HasIndex( l => l.Name).IsUnique();

            builder.HasMany(l => l.LeagueReferees)
                .WithOne(lr => lr.League)
                .HasForeignKey(lr => lr.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.Clubs)
                .WithOne(c => c.League)
                .HasForeignKey(c => c.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(l => l.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
