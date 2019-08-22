using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfGetLeagueCommand : EfBaseCommand, IGetLeagueCommand
    {
        public EfGetLeagueCommand(LigaContext context) : base(context)
        {
        }

        public LeagueDto Execute(int request)
        {
            var league = Context.Leagues.Include(l => l.Clubs).Include(l => l.LeagueReferees).Where( l => l.Id == request && l.IsDeleted == false).FirstOrDefault();

            if (league == null)
                throw new EntityNotFoundException("League");

            if (league.IsDeleted)
                throw new EntityNotFoundException("League");

            return new LeagueDto {
                Id = league.Id,
                ClubsId = league.Clubs.Select(c => c.Id).ToList(),
                Level = league.Level,
                Name = league.Name,
                RefereesId = league.LeagueReferees.Select(lr => lr.RefereeId)
            };
        }
    }
}
