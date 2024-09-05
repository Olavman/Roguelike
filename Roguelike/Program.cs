using Roguelike;
using System;
using CellularAutomataSystems;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GameCore
{
  internal class Program
  {
    static void Main(string[] args)
    {

      Game game = new Game();

      game.Run();

      Console.ReadKey();
    }
  }
}
