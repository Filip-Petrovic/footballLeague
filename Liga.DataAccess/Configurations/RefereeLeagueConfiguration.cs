using Liga.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.DataAccess.Configurations
{
    public class RefereeLeagueConfiguration : IEntityTypeConfiguration<RefereeLeague>
    {
        public void Configure(EntityTypeBuilder<RefereeLeague> builder)
        {
            builder.HasKey(rl => new { rl.RefereeId, rl.LeagueId });

            builder.HasOne(rl => rl.League)
                .WithMany(l => l.LeagueReferees)
                .HasForeignKey(rl => rl.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rl => rl.Referee)
                .WithMany(r => r.RefereeLeagues)
                .HasForeignKey(rl => rl.RefereeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
