using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfEditClubCommand : EfBaseCommand, IEditClubCommand
    {
        public EfEditClubCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(ClubDto request)
        {
            var club = Context.Clubs.Find(request.Id);

            if (club == null || club.IsDeleted)
                throw new EntityNotFoundException("Club");

            if (request.Name != null)
                club.Name = request.Name;
            if (request.LeagueId != 0)
            {
                if (!Context.Leagues.Any(l => l.Id == request.LeagueId))
                    throw new EntityNotFoundException("League");
                club.LeagueId = request.LeagueId;
            }
            if (request.CityId != 0)
            { 
                if (!Context.Cities.Any(l => l.Id == request.CityId))
                    throw new EntityNotFoundException("City");

                club.CityId = request.CityId;
            }
            Context.SaveChanges();
        }
    }
}
