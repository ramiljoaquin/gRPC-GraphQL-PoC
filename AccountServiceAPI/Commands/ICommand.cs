using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService
{
  public interface ICommand
  {
    bool IsValid { get; }
    IEnumerable<string> ValidationErrorMessages { get; }

    void Validate();
  }
}
