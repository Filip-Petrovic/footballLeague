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
    public class EfGetClubCommand : EfBaseCommand, IGetClubCommand
    {
        public EfGetClubCommand(LigaContext context) : base(context)
        {
        }

        public ClubDto Execute(int request)
        {
            var club = Context.Clubs.Include(c => c.Players).Where(c => c.Id == request).FirstOrDefault();

            if (club == null || club.IsDeleted)
                throw new EntityNotFoundException("Club");

            return new ClubDto
            {
                CityId = club.CityId,
                Id = club.Id,
                LeagueId = club.LeagueId,
                Name = club.Name,
                PlayersId = club.Players.Select(p => p.Id)
            };
        }
    }
}
