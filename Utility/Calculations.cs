using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
  public static class Calculations
  {
    public static double CalculateManhattanDistance(double x1, double y1, double x2, double y2)
    {
      double xx = Math.Abs(x1 - x2);
      double yy = Math.Abs(y1 - y2);

      return (xx + yy);
    }

    public static List<(int, int)> GetLinePoints(int x0, int y0, int x1, int y1, int maxLength = 999)
    {
      List<(int, int)> points = new List<(int, int)>();

      int startX = x0;
      int startY = y0;

      int dx = Math.Abs(x1 - x0);
      int dy = Math.Abs(y1 - y0);
      int sx = x0 < x1 ? 1 : -1; // Left or right
      int sy = y0 < y1 ? 1 : -1; // Up or down
      int err = dx - dy;

      while (true)
      {
        if (((startX - x0) * (startX - x0) + (startY - y0) * (startY - y0)) >= maxLength * maxLength) break; // Stop if line is longer than maxLength

        points.Add((x0, y0));

        if (x0 == x1 && y0 == y1) // Stop if line reached its goal
          break;


        int e2 = 2 * err;
        if (e2 > -dy)
        {
          err -= dy;
          x0 += sx;
        }
        if (e2 < dx)
        {
          err += dx;
          y0 += sy;
        }
      }


      return points;
    }

    private static bool IsWithinDistance(int x0, int y0, int x1, int y1, int maxLength)
    {
      int dx = Math.Abs( x0 - x1);
      int dy = Math.Abs(y0 - y1);
      return (dx * dx + dy * dy) <= (maxLength * maxLength);
    }
  }
}
