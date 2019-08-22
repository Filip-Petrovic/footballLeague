using Application.Commands;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfDeleteClubCommand : EfBaseCommand, IDeleteClubCommand
    {
        public EfDeleteClubCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var club = Context.Clubs.Find(request);

            if (club == null || club.IsDeleted)
                throw new EntityNotFoundException();

            club.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
