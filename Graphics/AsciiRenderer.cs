using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Entities;
using GridSystems;

namespace Graphics
{

  public class AsciiRenderer
  {
    #region// Enums

    #endregion

    #region// Fields
    #endregion

    #region// Properties
    public AsciiRenderer(int width, int height) 
    {
      Console.CursorVisible = false;
    }
    #endregion

    #region// Constructors

    #endregion

    #region// Methods
    public void RenderMap (Grid grid, List<Entity> entities, HashSet<Cell> visibleCells, int startY)
    {
      StringBuilder buffer = new StringBuilder();

      // Level
      for (int y = 0; y < grid.Height; y++)
      {
        for (int x = 0; x < grid.Width; x++)
        {
          char renderChar = ' ';
          Cell cell = grid.GetCell(x, y);
          if (visibleCells.Contains(cell))
          {
            renderChar = cell.ToChar();
          }
          buffer.Append(renderChar);
        }
        buffer.AppendLine(); // New line for each row
      }

      // Overlay entities on the buffer
      foreach(Entity entity in entities)
      {
        if (grid.IsInsideGrid(entity.X, entity.Y))
        {
          int bufferPosition = (entity.Y * (grid.Width + Environment.NewLine.Length)) + entity.X;
          buffer[bufferPosition] = entity.Symbol;
        }
      }
      
      // Write the buffer to the console in one operation
      Console.SetCursorPosition(0, startY);
      Console.Write(buffer.ToString());
    }
    #endregion
  }
}
