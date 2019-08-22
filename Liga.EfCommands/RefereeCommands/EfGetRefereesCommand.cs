using Application.Commands;
using Application.DataTransfer;
using Application.Interfaces;
using Application.Searches;
using Liga.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfGetRefereesCommand : EfBaseCommand, IGetRefereesCommand
    {
        public EfGetRefereesCommand(LigaContext context): base(context)
        {

        }
        public IEnumerable<RefereeDto> Execute(RefereeSearch request)
        {
            var query = Context.Referees.AsQueryable();

            if(request.FirstName != null)
            {
                var keywordFirstName = request.FirstName.ToLower();
                query = query.Where(r => r.FirstName.ToLower().Contains(keywordFirstName));
            }
            if(request.LastName != null)
            {
                var keywordLastName = request.LastName.ToLower();
                query = query.Where(r => r.LastName.ToLower().Contains(keywordLastName));
            }

            var response = query.Include(r => r.RefereeLeagues).ThenInclude(rl => rl.League).Where(r => r.IsDeleted == false).Select(r => new RefereeDto
            {
                FirstName = r.FirstName,
                Id = r.Id,
                LastName = r.LastName,
                LeaguesId = r.RefereeLeagues.Select(rl => rl.League).Select(l => l.Id)
                
            });
            return response;
        }


    }
}
