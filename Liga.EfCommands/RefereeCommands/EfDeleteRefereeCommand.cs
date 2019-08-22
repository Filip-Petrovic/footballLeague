using Application.Commands;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfDeleteRefereeCommand : EfBaseCommand, IDeleteRefereeCommand
    {
        public EfDeleteRefereeCommand(LigaContext context): base(context)
        {

        }
        public void Execute(int request)
        {
            var referee = Context.Referees.Find(request);
            if (referee == null)
                throw new EntityNotFoundException("Referee");
            if(referee.IsDeleted)
                throw new EntityNotFoundException("Referee");

            referee.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
