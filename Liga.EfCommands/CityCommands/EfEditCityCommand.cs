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
    public class EfEditCityCommand : EfBaseCommand, IEditCityCommand
    {
        public EfEditCityCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(CityDto request)
        {
            var city = Context.Cities.Find(request.Id);

            if (city.IsDeleted)
                throw new EntityNotFoundException("city");

            if (Context.Cities.Any(c => c.PostalCode == request.PostalCode))
                throw new EntityAlreadyExistsException("City");

            if (Context.Cities.Any(c => c.Name == request.Name))
                throw new EntityAlreadyExistsException("City");

            if (request.Name != null)
                city.Name = request.Name;

            if (request.PostalCode != 0)
                city.PostalCode = request.PostalCode;

            Context.SaveChanges();


        }
    }
}
