using GridSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomataSystems
{
  public class CaveGenerationRule : ICellularAutomataRule
  {
    public void ApplyRule(Cell cell, ref Cell[,] gridCopy, Grid trueGrid)
    {
      int neighbours = CheckNeighbours(cell, trueGrid);
      if (neighbours < 3)
      {
        gridCopy[cell.X, cell.Y].State = 1;
        gridCopy[cell.X, cell.Y].IsWalkable = true;
      }
      else if (neighbours > 6)
      {
        gridCopy[cell.X, cell.Y].State = 0;
        gridCopy[cell.X, cell.Y].IsWalkable = false;
      }

    }

    public dynamic CheckNeighbours(Cell cell, Grid trueGrid)
    {
      int count = 0;
      for (int x = cell.X-1; x <= cell.X+1; x++)
      {
        for (int y = cell.Y-1; y <= cell.Y+1; y++)
        {
          if (!trueGrid.IsInsideGrid(x, y)) return 8;
          if ((x == cell.X && y == cell.Y)) continue;
          else
          {
            // Count active neighbours
            if (trueGrid.Cells[x, y].State == 0) count++;
          }
        }
      }
      return count;
    }
  }
}
