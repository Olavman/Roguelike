using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystems
{
  public static class FloodFill
  {
    public static void Fill(Grid grid, int startX, int startY, int roomID, dynamic startState)
    {
      if (!IsValidCell(grid, startX, startY, roomID, startState))
      {
        return;
      }

      // Set node
      grid.Cells[startX, startY].RoomID = roomID; // Set room ID to cell
      grid.Rooms[roomID].Cells.Add(grid.Cells[startX, startY]); // Add cell to the room object Cells list

      Fill (grid, startX+1, startY, roomID, startState); // Check right
      Fill (grid, startX-1, startY, roomID, startState); // Check left
      Fill (grid, startX, startY+1, roomID, startState); // Check down
      Fill (grid, startX, startY-1, roomID, startState); // Check up
    }
    public static bool IsValidCell(Grid grid, int x, int y, int roomID, dynamic startState)
    {
      // Returns false if outside grid or part of the same roomID or state of current cell is not equal state of start cell
      if (!grid.IsInsideGrid(x, y) || grid.Cells[x,y].RoomID == roomID || grid.Cells[x, y].State != startState ) return false;

      return true;
    }
  }
}
