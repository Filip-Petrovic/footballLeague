using Application.DataTransfer;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Application.Commands
{
    public interface IGetRefereeCommand: ICommand<int, RefereeDto>
    {
    }
}
