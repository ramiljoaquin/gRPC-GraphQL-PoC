using Autofac;
using AccountService;
using AccountService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Commands
{
  public class ProfileCommandBus : CommandBus<ProfileDbContext>
  {
    public ProfileCommandBus(ILifetimeScope scope) : base(scope)
    {
    }
  }
}
