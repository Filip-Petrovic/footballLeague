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
    public class EfGetCitiesCommand : EfBaseCommand, IGetCitiesCommand

    {
        public EfGetCitiesCommand(LigaContext context) : base(context)
        {
        }

        public IEnumerable<CityDto> Execute(CitySearch request)
        {
            var query = Context.Cities.Include(c => c.Players).Include(c => c.Clubs).AsQueryable().Where(c => c.IsDeleted == false);

            if (request.Name != null)
                query = query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));
            if (request.PostalCode != 0)
                query = query.Where(c => c.PostalCode == request.PostalCode);

            return query.Where(c => c.IsDeleted == false).Select(c => new CityDto {
                ClubsId = c.Clubs.Select(cl => cl.Id),
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode,
                PlayersId = c.Players.Select(p => p.Id)
            });
        }
    }
}
