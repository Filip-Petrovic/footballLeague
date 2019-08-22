using Application.Commands;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfDeletePlayerCommand : EfBaseCommand, IDeletePlayerCommand
    {
        public EfDeletePlayerCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var player = Context.Players.Find(request);

            if (player == null)
                throw new EntityNotFoundException("Playeer");
            if(player.IsDeleted == true)
                throw new EntityNotFoundException("Playeer");

            player.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
