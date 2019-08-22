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
    public class EfAddLeagueCommand : EfBaseCommand, IAddLeagueCommand
    {
        public EfAddLeagueCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(LeagueDto request)
        {
            if (Context.Leagues.Any(l => l.Name == request.Name))
                throw new EntityAlreadyExistsException("league");

            List<RefereeLeague> lista = new List<RefereeLeague>();

            if(request.RefereesId != null)
            {
                foreach (var refereeId in request.RefereesId)
                {
                    if (!Context.Referees.Any(l => l.Id == refereeId))
                        throw new EntityNotFoundException("Referee");

                    lista.Add(new RefereeLeague { LeagueId = request.Id, RefereeId = refereeId });
                }
            }

            Context.Leagues.Add(new League {
                LeagueReferees = lista,

                Level = request.Level,
                IsDeleted = false,
                Name = request.Name
            });
            Context.SaveChanges();
        }
    }
}
