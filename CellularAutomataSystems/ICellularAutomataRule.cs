using GridSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomataSystems
{
  public interface ICellularAutomataRule
  {
    void ApplyRule(Cell cell, ref Cell[,] grid, Grid trueGrid);
    dynamic CheckNeighbours(Cell cell, Grid grid);
  }
}
