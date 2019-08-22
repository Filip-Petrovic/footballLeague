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
    public class EfGetPositionCommand : EfBaseCommand, IGetPositionCommand
    {
        public EfGetPositionCommand(LigaContext context) : base(context)
        {
        }

        public PositionDto Execute(int request)
        {
            var query = Context.Positions.AsQueryable();
            var position = query.Include( p => p.Players).Where(p => p.Id == request && p.IsDeleted == false).FirstOrDefault();

            if (position == null)
                throw new EntityNotFoundException("Position");

            return new PositionDto
            {
                Id = position.Id,
                Name = position.Name,
                PlayersId = position.Players.Select(p => p.Id)
            };
        }
    }
}
