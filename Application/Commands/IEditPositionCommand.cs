using Application.DataTransfer;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Application.Commands
{
    public interface IEditPositionCommand: ICommand<PositionDto>
    {
    }
}
