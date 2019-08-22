using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using Liga.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfAddClubCommand : EfBaseCommand, IAddClubCommand
    {
        public EfAddClubCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(ClubDto request)
        {
            if (!Context.Leagues.Any(l => l.Id == request.LeagueId))
                throw new EntityNotFoundException("League");
            if (!Context.Cities.Any(l => l.Id == request.CityId))
                throw new EntityNotFoundException("city");
            Context.Clubs.Add(new Club {
                Name = request.Name,
                CityId = request.CityId,
                IsDeleted = false,
                LeagueId = request.LeagueId 
            });
            Context.SaveChanges();
        }
    }
}
