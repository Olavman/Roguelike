using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridSystems;

namespace CellularAutomataSystems
{
  public static class CellularAutomata
  {
    #region// Enums

    #endregion

    #region// Fields
    #endregion

    #region// Properties

    #endregion

    #region// Constructors

    #endregion

    #region// Methods

    public static void ApplySpesificRule(Grid grid, ICellularAutomataRule rule, int iterations)
    {
      for (int i = 0; i < iterations; i++)
      {
        // Copy grid
        Cell[,] gridCopy = Grid.DeepCopyCells(grid.Cells);

        // Update cells on copied grid
        for (int x = grid.Width-1; x >= 0; x--)
        {
          for (int y = grid.Height-1; y >= 0; y--)
          {
            rule.ApplyRule(grid.Cells[x, y], ref gridCopy, grid);
          }
        }
        // Overwrite grid
        grid.Cells = Grid.DeepCopyCells(gridCopy);
      }
    }
    public static void ApplyInheritRule()
    {

    }
    #endregion
  }
}
