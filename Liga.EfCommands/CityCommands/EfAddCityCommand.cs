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
    public class EfAddCityCommand : EfBaseCommand, IAddCityCommand
    {
        public EfAddCityCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(CityDto request)
        {

            if (Context.Cities.Any(c => c.PostalCode == request.PostalCode))
                throw new EntityAlreadyExistsException("City");

            if (Context.Cities.Any(c => c.Name == request.Name))
                throw new EntityAlreadyExistsException("City");

            Context.Cities.Add(new City {

                IsDeleted= false,
                Name = request.Name,
                PostalCode = request.PostalCode
            });

            Context.SaveChanges();
        }
    }
}
