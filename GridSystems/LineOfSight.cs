using GridSystems;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utility;

namespace GridSystems
{
  public class LineOfSight
  {
    public int MaxViewDistance { get; set; }
    public LineOfSight(int maxViewDistance)
    {
      MaxViewDistance = maxViewDistance;
    }

    public HashSet<Cell> GetVisibleCells(Grid grid, int startX, int startY)
    {
      var visibleCells = new HashSet<Cell>();
      int minX = startX - MaxViewDistance;
      int maxX = startX + MaxViewDistance;
      int minY = startY - MaxViewDistance;
      int maxY = startY + MaxViewDistance;

      // Top edge
      for (int x = minX; x <= maxX; x++)
      {
        CheckAndAddVisibleCell(grid, visibleCells, x, minY, startX, startY, MaxViewDistance);
      }

      // Right edge
      for (int y = minY + 1; y <= maxY; y++)
      {
        CheckAndAddVisibleCell(grid, visibleCells, maxX, y, startX, startY, MaxViewDistance);
      }

      // Bottom edge
      for (int x = maxX - 1; x >= minX; x--)
      {
        CheckAndAddVisibleCell(grid, visibleCells, x, maxY, startX, startY, MaxViewDistance);
      }

      // Left edge
      for (int y = maxY - 1; y > minY; y--)
      {
        CheckAndAddVisibleCell(grid, visibleCells, minX, y, startX, startY, MaxViewDistance);
      }

      return visibleCells;
    }
    private void CheckAndAddVisibleCell(Grid grid, HashSet<Cell> visibleCells, int cellX, int cellY, int startX, int startY, int MaxViewDistance)
    {
      var line = Calculations.GetLinePoints(startX, startY, cellX, cellY, MaxViewDistance);
      foreach (var point in line)
      {
        if (grid.IsInsideGrid(point.Item1, point.Item2))
        {
          Cell cell = grid.GetCell(point.Item1, point.Item2);
          if (!visibleCells.Contains(cell))
          {
            visibleCells.Add(cell);
          }
          if (cell.State == 0) // If obstacle
          {
            break;
          }
        }
      }
    }

  }
}