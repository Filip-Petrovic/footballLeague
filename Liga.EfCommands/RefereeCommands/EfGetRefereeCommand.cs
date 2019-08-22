using System;
using System.Collections.Generic;
using System.Text;
using Liga.DataAccess;
using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Liga.EfCommands
{
    public class EFGetRefereeCommand : EfBaseCommand, IGetRefereeCommand
    {
        public EFGetRefereeCommand(LigaContext context): base(context)
        {

        }
        public RefereeDto Execute(int request)
        {
            var query = Context.Referees.AsQueryable();
            var referee = query.Include(r => r.RefereeLeagues)
                .ThenInclude(rl => rl.League)
                .Where(r => r.Id == request && r.IsDeleted == false)
                .FirstOrDefault();

            if (referee == null)
                throw new EntityNotFoundException("Referee");

            return new RefereeDto
            {
                FirstName = referee.FirstName,
                Id = referee.Id,
                LastName = referee.LastName,
                LeaguesId = referee.RefereeLeagues.Select(rl => rl.League).Select(l => l.Id)

            };
        }
    }
}
