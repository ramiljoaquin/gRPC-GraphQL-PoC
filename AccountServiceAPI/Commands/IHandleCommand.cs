using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService
{
  public interface IHandleCommand { }

  public interface IHandleCommand<in TCommand> : IHandleCommand
      where TCommand : ICommand
  {
    Task<CommandHandlerResult> HandleAsync(TCommand command);
  }
}
