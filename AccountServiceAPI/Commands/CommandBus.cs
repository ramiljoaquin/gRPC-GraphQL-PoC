using System.Data;
using System.Threading;
using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Security;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService
{
  public abstract class CommandBus<TDb> : ICommandBus
      where TDb : DbContext
  {
    private const string SCOPE_NAME = "CommandHandlers";
    private readonly ILifetimeScope _scope;
    private CommandHandlerResult _innerOkResult;

    public CommandBus(ILifetimeScope scope)
    {
      if (scope == null)
      {
        throw new ArgumentNullException("scope");
      }
      _scope = scope;
    }

    public CommandResult GetDelayedCommandResult()
    {
      if (_innerOkResult == null || !_innerOkResult.HasDelayedContent)
      {
        return GetCommandResultFromCommandHandlerResult(_innerOkResult);
      }

      _innerOkResult.ResolveAndUpdateDelayedContent();
      return GetCommandResultFromCommandHandlerResult(_innerOkResult);
    }


    public async Task<CommandResult> SendAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
      try
      {
        var innerHandler = (IHandleCommand<TCommand>)
            _scope.ResolveOptional(typeof(IHandleCommand<TCommand>));

        if (innerHandler != null)
        {
          command.Validate();

          if (command.IsValid)
          {
            _innerOkResult = await innerHandler.HandleAsync(command);

            if (_innerOkResult == null || !_innerOkResult.HasDelayedContent)
            {
              return GetCommandResultFromCommandHandlerResult(_innerOkResult);
            }

            _innerOkResult.ResolveAndUpdateDelayedContent();

            return GetCommandResultFromCommandHandlerResult(_innerOkResult);
          }
          else
          {
            return CommandResult.FromValidationErrors(command.ValidationErrorMessages);

          }
        }

        return CommandResult.NonExistentCommand(typeof(TCommand).Name);
      }
      catch (SecurityException ex)
      {
        return new CommandResult(ex, HttpStatusCode.Forbidden);
      }
      catch (Exception ex)
      {
        return new CommandResult(ex, HttpStatusCode.InternalServerError);
      }
    }


    public async Task<CommandResult> TransactionSendAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
      var context = _scope.Resolve<TDb>();
      using (var transaction = await context.Database.BeginTransactionAsync())
      {
        try
        {
          var result = await SendAsync<TCommand>(command);
          if (result.IsOk)
          {
            result = GetDelayedCommandResult();

            await context.SaveChangesAsync();

            await transaction.CommitAsync();

          }
          else
          {
            result.Message = result.Value?.ToString();

            await transaction.RollbackAsync();
          }
          return result;

        }
        catch (Exception ex)
        {
          await transaction.RollbackAsync();

          return new CommandResult(ex, HttpStatusCode.InternalServerError);
        }
      }
    }


    private CommandResult GetCommandResultFromCommandHandlerResult(CommandHandlerResult result)
    {
      if (result == CommandHandlerResult.Ok)
      {
        return CommandResult.Ok;
      }

      var content = result.Content;
      return new CommandResult(result.StatusCode, content);
    }

    public async Task<int> ApplyChangesAsync()
    {
      return await _scope.Resolve<TDb>().SaveChangesAsync();
    }
  }
}
