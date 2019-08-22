using Application.Commands;
using Application.Exceptions;
using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public class EfDeleteCityCommand : EfBaseCommand, IDeleteCityCommand
    {
        public EfDeleteCityCommand(LigaContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var city = Context.Cities.Find(request);
            if (city == null || city.IsDeleted)
                throw new EntityNotFoundException("City");

            city.IsDeleted = true;
            Context.SaveChanges();


        }
    }
}
