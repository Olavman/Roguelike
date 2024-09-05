using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridSystems;
using Utility;

namespace LevelGenerationSystems
{
  public static class RoomConnector
  {
    public static void ConnectRooms(Grid grid)
    {
      int index = 0;
      foreach (Room room in grid.Rooms)
      {
        Cell[] cells = RandomCellInNextRoom(index, grid.Rooms);
        CreateLShapedCorridor(cells[0], cells[1], grid, RandomGenerator.NextBool());

        index++;
      }
    }
    public static Room FindNearestRoom(Room room, List<Room> rooms)
    {
      double clostestDistance = 0;
      Room nearestRoom = null;
      foreach (Room room1 in rooms)
      {
        if (room1 == room || room1.Cells.Count < 4) 
        {
          continue;
        }

        double x1 = room.GetAverageXPosition();
        double x2 = room1.GetAverageXPosition();
        double y1 = room.GetAverageYPosition();
        double y2 = room1.GetAverageYPosition();

        double distance = Calculations.CalculateManhattanDistance(x1, y1, x2, y2);

        if (nearestRoom == null || distance < clostestDistance)
        {
          nearestRoom = room1;
          clostestDistance = distance;
        }
      }
      Console.WriteLine(clostestDistance.ToString());
      return nearestRoom;
    }
    public static Cell[] FindNearestRandomCell(Room room1, List<Room> rooms)
    {
      Cell[] cells = new Cell[2];
      int closestDistance = 0;

      // Pick a random cell in the first room
      cells[0] = room1.Cells[RandomGenerator.NextInt(0, room1.Cells.Count-1)];

      // Pick a random cell in every other room and find the closest one
      foreach(Room room2 in rooms)
      {
        if (room1 == room2 || room2.Cells.Count < 4) // Skip if its the same room or the room is too small
        { 
          continue; 
        }

        int index = RandomGenerator.NextInt(0, room2.Cells.Count-1);
        Cell cell2 = room2.Cells[index];

        int distance = (int)Calculations.CalculateManhattanDistance(cells[0].X, cells[0].Y, cell2.X, cell2.Y);

        if (cells[1] == null || distance < closestDistance)
        {
          cells[1] = cell2;
          closestDistance = distance;
        }
      }
      return cells;
    }
    public static Cell[] RandomCellInNextRoom(int index, List<Room> rooms)
    {
      Cell[] cells = new Cell[2];

      Room room1 = null;
      Room room2 = null;

      if (index+1 >= rooms.Count) 
      {
        return cells;
      }
        room1 = rooms[index];
        room2 = rooms[index+1];

      // Pick a random cell in the current room
      cells[0] = room1.Cells[RandomGenerator.NextInt(0, room1.Cells.Count()-1)];
      
      // Pick a random cell in the next room
      cells[1] = room2.Cells[RandomGenerator.NextInt(0, room2.Cells.Count()-1)];

      return cells;
    }
    public static void CreateLShapedCorridor(Cell cell1, Cell cell2, Grid grid, bool horizontalFirst)
    {
      if (cell1 == null || cell2 == null) { return; }

      int x = cell1.X;
      int y = cell1.Y;

      int endX = cell2.X;
      int endY = cell2.Y;

      int dx = Math.Sign(endX - x);
      int dy = Math.Sign(endY - y);

      //Console.WriteLine("x: " + x + "-" + endX + " y: " + y + "-" + endY + " Room: " + cell1.RoomID + "-" + cell2.RoomID);

      while (x != endX || y != endY)
      {
        if (!grid.Cells[x, y].IsRoom()) // Only change the cell if its not part of a room already
        {
          Cell cell = grid.Cells[x, y];
          cell.State = cell1.State;
          cell.IsWalkable = cell1.IsWalkable;
        }

        if (horizontalFirst) // Horizontal corridor first
        {
          if (x != endX)
          { 
            x += dx;
          }
          else if (y != endY)
          {
            y += dy;
          }
        }
        else if (!horizontalFirst) // Vertical corridor first
        {
          if (y != endY)
          {
            y += dy;
          }
          else if (x != endX)
          {
            x += dx;
          }
        }

      }
    }
  }
}
