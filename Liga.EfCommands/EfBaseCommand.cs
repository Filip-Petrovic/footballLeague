using Liga.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.EfCommands
{
    public abstract class EfBaseCommand
    {
        
            protected LigaContext Context { get; set; }
            protected EfBaseCommand(LigaContext context) => Context = context;
       
    }
}
