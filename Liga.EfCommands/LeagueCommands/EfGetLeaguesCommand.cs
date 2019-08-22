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
    public class EfGetLeaguesCommand : EfBaseCommand, IGetLeaguesCommand
    {
        public EfGetLeaguesCommand(LigaContext context) : base(context)
        {
        }

        public IEnumerable<LeagueDto> Execute(LeagueSearch request)
        {
            var query = Context.Leagues.Include(l => l.Clubs).Include(l => l.LeagueReferees).AsQueryable();

            if (request.Level != 0)
                query = query.Where(l => l.Level == request.Level);
            if (request.Name != null)
                query = query.Where(l => l.Name.ToLower().Contains(request.Name.ToLower()));

            return query.Where(l => l.IsDeleted == false).Select(l => new LeagueDto {
                RefereesId = l.LeagueReferees.Select( rl => rl.RefereeId),
                ClubsId = l.Clubs.Select(c => c.Id),
                Id = l.Id,
                Level = l.Level,
                Name = l.Name
            }).ToList();

        }
    }
}
