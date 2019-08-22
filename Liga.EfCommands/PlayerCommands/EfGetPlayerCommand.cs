using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using Liga.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfGetPlayerCommand : EfBaseCommand, IGetPlayerCommand
    {
        public EfGetPlayerCommand(LigaContext context) : base(context)
        {
        }

        public PlayerDto Execute(int request)
        {
            var player = Context.Players.Find(request);

            if (player == null)
                throw new EntityNotFoundException("Player");

            if (player.IsDeleted == true)
                throw new EntityNotFoundException("Player");

            return new PlayerDto
            {
                CityId = player.CityId,
                ClubId = player.ClubId,
                Email = player.Email,
                FirstName = player.FirstName,
                Id = player.Id,
                Image = player.Image,
                LastName = player.LastName,
                PositionId = player.PositionId
                
            };
        }
    }
}
