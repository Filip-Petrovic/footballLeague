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
    public class EfAddPositionCommand : EfBaseCommand, IAddPositionCommand
    {
        public EfAddPositionCommand(LigaContext context) : base(context)
        {
        }
        public void Execute(PositionDto request)
        {
            if (Context.Positions.Any(p => p.Name.ToLower() == request.Name.ToLower()))
                throw new EntityAlreadyExistsException("Position ");

            Context.Positions.Add(new Domain.Position
            {
                IsDeleted = false,
                Name = request.Name

            });
            Context.SaveChanges();
        }
    }
}
