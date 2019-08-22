using Application.Commands;
using Application.DataTransfer;
using Application.Searches;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfGetPlayersCommand : EfBaseCommand, IGetPlayersCommand
    {
        public EfGetPlayersCommand(LigaContext context) : base(context)
        {
        }

        public IEnumerable<PlayerDto> Execute(PlayerSearch request)
        {
            var query = Context.Players.AsQueryable();

            if (request.PositionId != 0)
                query = query.Where(p => p.PositionId == request.PositionId);
            if (request.LastName != null)
                query = query.Where(p => p.LastName.ToLower().Contains(request.LastName.ToLower()));
            if(request.FirstName != null)
                query = query.Where(p => p.FirstName.ToLower().Contains(request.FirstName.ToLower()));
            if(request.Email != null)
                query = query.Where(p => p.Email.ToLower().Contains(request.Email.ToLower()));
            if(request.ClubId != 0)
                query = query.Where(p => p.ClubId == request.ClubId);
            if(request.CityId != 0)
                query = query.Where(p => p.CityId == request.CityId);

            return query.Where(p=>p.IsDeleted == false ).Select(p => new PlayerDto {
                CityId = p.CityId,
                ClubId = p.ClubId,
                Email = p.Email,
                FirstName =p.FirstName,
                Id = p.Id,
                Image = p.Image,
                LastName = p.LastName,
                PositionId = p.PositionId
            });
        }
    }
}
