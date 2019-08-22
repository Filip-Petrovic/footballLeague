using Liga.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.DataAccess.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(p => p.Players)
                .WithOne(pl => pl.Position)
                .HasForeignKey(pl => pl.PositionId);

            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
