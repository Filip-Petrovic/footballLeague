using Application.Commands;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfDeletePositionCommand : EfBaseCommand, IDeletePositionCommand
    {
        public EfDeletePositionCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var position = Context.Positions.Find(request);
            if (position == null)
                throw new EntityNotFoundException("Position");
            if (position.IsDeleted)
                throw new EntityNotFoundException("Position");

            position.IsDeleted = true;

            
            Context.SaveChanges();

        }
    }
}
