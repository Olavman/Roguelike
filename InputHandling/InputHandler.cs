using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputHandling
{
  public class InputHandler
  {
    public event Action<char> OnCommandRecieved;

    public void ProcessInput()
    {
      char command = Console.ReadKey(true).KeyChar;
      OnCommandRecieved?.Invoke(command);
    }
  }
}
