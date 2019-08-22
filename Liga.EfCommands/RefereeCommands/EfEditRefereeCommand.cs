using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using Liga.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfEditRefereeCommand : EfBaseCommand, IEditRefereeCommand
    {
        public EfEditRefereeCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(RefereeDto request)
        {
            var referee = Context.Referees.Find(request.Id);

            if (referee == null)
                throw new EntityNotFoundException("Referee");
            List<RefereeLeague> lista = new List<RefereeLeague>();
            if (request.LeaguesId != null)
            {
                foreach (var leagueId in request.LeaguesId)
                {
                    if (!Context.Leagues.Any(l => l.Id == leagueId))
                        throw new EntityNotFoundException("League");

                    lista.Add(new RefereeLeague { LeagueId = leagueId, RefereeId = request.Id });
                }
                referee.RefereeLeagues = lista;


            }
            if (request.LastName != referee.LastName && request.LastName != null)
                referee.LastName = request.LastName;
            if (request.FirstName != referee.FirstName && request.FirstName != null) 
                referee.FirstName = request.FirstName;

            Context.SaveChanges();
        }
    }
}
