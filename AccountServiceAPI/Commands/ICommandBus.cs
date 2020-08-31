using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService
{
  public interface ICommandBus
  {
    Task<CommandResult> SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    Task<CommandResult> TransactionSendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    Task<int> ApplyChangesAsync();
    CommandResult GetDelayedCommandResult();
  }
}
