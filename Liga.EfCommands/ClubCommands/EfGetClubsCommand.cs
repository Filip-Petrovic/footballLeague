using Application.Commands;
using Application.DataTransfer;
using Application.Searches;
using Liga.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfGetClubsCommand : EfBaseCommand, IGetClubsCommand
    {
        public EfGetClubsCommand(LigaContext context) : base(context)
        {
        }

        public IEnumerable<ClubDto> Execute(ClubSearch request)
        {
            var query = Context.Clubs.Include(c => c.Players).AsQueryable().Where(c => c.IsDeleted == false);

            if (request.Name != null)
                query = query.Where( c => c.Name.ToLower().Contains(request.Name.ToLower()));

            if (request.LeagueId != 0)
                query = query.Where(c => c.LeagueId == request.LeagueId);

            if (request.CityId != 0)
                query = query.Where(c => c.CityId == request.CityId);

            return query.Select(c => new ClubDto {
                CityId = c.CityId,
                LeagueId = c.LeagueId,
                Id = c.Id,
                Name = c.Name,
                PlayersId = c.Players.Select(p => p.Id)
            });
        }
    }
}
