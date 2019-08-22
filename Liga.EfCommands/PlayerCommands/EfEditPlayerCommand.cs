using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfEditPlayerCommand : EfBaseCommand, IEditPlayerCommand
    {
        public EfEditPlayerCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(PlayerDto request)
        {
            var player = Context.Players.Find(request.Id);

            if (player == null)
                throw new EntityNotFoundException();

            if (request.Image != null)
                player.Image = request.Image;

            if (request.LastName != null)
                player.LastName = request.LastName;


            if (request.PositionId != 0)
            {
                if (!Context.Positions.Any(p => p.Id == request.PositionId))
                    throw new EntityNotFoundException("Position");

                player.PositionId = request.PositionId;

            }

            if (request.FirstName != null)
                player.FirstName = request.FirstName;

            if (request.Email != null)
                player.Email = request.Email;


            if (request.ClubId != null )
            {
                if (!Context.Clubs.Any(p => p.Id == request.ClubId))
                    throw new EntityNotFoundException("Club");
                player.ClubId = request.ClubId;

            }


            if (request.CityId != 0)
            {
                if (!Context.Cities.Any(p => p.Id == request.CityId))
                    throw new EntityNotFoundException("City");

                player.CityId = request.CityId;
            }


            Context.SaveChanges();
        }
    }
}
