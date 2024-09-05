using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystems
{
  public class Room
  {
    public int ID {  get; set; }
    public List<Cell> Cells;

    public Room (int id)
    {
      this.ID = id;
      Cells = new List<Cell>();
    }

    public double GetAverageXPosition()
    {
      if (Cells.Count == 0)
      {
        return 0;
      }

      return Cells.Average(cell => cell.X);
    }
    public double GetAverageYPosition()
    {
      if (Cells.Count == 0)
      {
        return 0;
      }

      return Cells.Average(cell => cell.Y);
    }
    public int[,] GetCenterOfRoom()
    {
      int x = (int) Math.Round(GetAverageXPosition());
      int y = (int) Math.Round(GetAverageYPosition());
      return new int[x, y];
    }
  }
}
