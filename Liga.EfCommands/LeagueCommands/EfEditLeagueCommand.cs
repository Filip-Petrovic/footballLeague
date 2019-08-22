using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using Liga.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfEditLeagueCommand : EfBaseCommand, IEditLeagueCommand
    {
        public EfEditLeagueCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(LeagueDto request)
        {
            var league = Context.Leagues.Include(l => l.Clubs).Include(l => l.LeagueReferees).Where(l => l.Id == request.Id && l.IsDeleted == false).FirstOrDefault();
        
            if (request.Level != 0)
                league.Level = request.Level;
            if (request.Name != null)
            {
                    if (league.Name.ToLower() == request.Name.ToLower())
                    throw new EntityAlreadyExistsException("league");
                league.Name = request.Name;
            }

            List<RefereeLeague> lista = new List<RefereeLeague>();

            if (request.RefereesId != null)
            {
                foreach (var refereeId in request.RefereesId)
                {
                    if (!Context.Referees.Any(l => l.Id == refereeId))
                        throw new EntityNotFoundException("Referee");

                    lista.Add(new RefereeLeague { LeagueId = request.Id, RefereeId = refereeId });
                }
                league.LeagueReferees = lista;
            }


            Context.SaveChanges();
        }
    }
}
