using Application.Commands;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfDeleteLeagueCommand : EfBaseCommand, IDeleteLeagueCommand
    {
        public EfDeleteLeagueCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var league = Context.Leagues.Find(request);

            if (league == null || league.IsDeleted)
                throw new EntityNotFoundException("League");

            league.IsDeleted = true;
            Context.SaveChanges();

        }
    }
}
