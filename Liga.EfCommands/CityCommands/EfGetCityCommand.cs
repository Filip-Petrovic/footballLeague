using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Liga.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liga.EfCommands
{
    public class EfGetCityCommand : EfBaseCommand, IGetCityCommand
    {
        public EfGetCityCommand(LigaContext context) : base(context)
        {
        }

        public CityDto Execute(int request)
        {
            var city = Context.Cities.Include( c => c.Clubs).Include(c => c.Players).Where(c => c.Id == request).FirstOrDefault();

            if (city == null || city.IsDeleted == true)
                throw new EntityNotFoundException("City");

            return new CityDto {
                Id = city.Id,
                ClubsId = city.Clubs.Select(c => c.Id),
                Name = city.Name,
                PostalCode = city.PostalCode,
                PlayersId = city.Players.Select(c => c.Id)
            };

        }
    }
}
