using Application.Commands;
using Application.DataTransfer;
using Liga.DataAccess;
using Liga.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfAddPlayerCommand : EfBaseCommand, IAddPlayerCommand
    {
        public EfAddPlayerCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(PlayerDto request)
        {
            Context.Players.Add(new Player {
                CityId = request.CityId,
                ClubId = request.ClubId,
                Email = request.Email,
                FirstName = request.FirstName,
                //Image = request.Image,
                LastName = request.LastName,
                Password = request.Password,
                PositionId = request.PositionId,
                IsDeleted = false
            });
            Context.SaveChanges();
        }
    }
}
