using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfEditPositionCommand : EfBaseCommand, IEditPositionCommand
    {
        public EfEditPositionCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(PositionDto request)
        {
            var position = Context.Positions.Find(request.Id);
            if (position == null)
                throw new EntityNotFoundException("Position");

            if (position.Name != request.Name)
                position.Name = request.Name;

            Context.SaveChanges();

        }
    }
}
