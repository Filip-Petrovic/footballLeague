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
    public class EfAddRefereeCommand : EfBaseCommand, IAddRefereeCommand
    {
        public EfAddRefereeCommand(LigaContext context) : base(context)
        {

        }
        public void Execute(RefereeDto request)
        {
            List<RefereeLeague> lista = new List<RefereeLeague>();
            if(request.LeaguesId != null)
            {
                foreach (var leagueId in request.LeaguesId)
                {
                    if (!Context.Leagues.Any(l => l.Id == leagueId))
                        throw new EntityNotFoundException("League");

                    lista.Add(new RefereeLeague { LeagueId = leagueId, RefereeId = request.Id });
                }

            }
            Context.Referees.Add(new Domain.Referee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsDeleted = false,
                RefereeLeagues = lista
            });

            
            
            Context.SaveChanges();

        }
    }
}
