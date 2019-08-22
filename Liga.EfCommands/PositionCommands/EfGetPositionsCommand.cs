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
    public class EfGetPositionsCommand : EfBaseCommand, IGetPositionsCommand
    {
        public EfGetPositionsCommand(LigaContext context) : base(context)
        {
        }

        public IEnumerable<PositionDto> Execute(PositionSearch request)
        {
            
            var query = Context.Positions.AsQueryable();
             if(request.Name != null)
            {
                var keyword = request.Name.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(keyword));
            }

            return query.Include( p => p.Players).Where(p => p.IsDeleted == false).Select(p => new PositionDto {
                 Id = p.Id,
                 Name = p.Name,
                 PlayersId = p.Players.Select( pl => pl.Id)
             });
        }
    }
}
