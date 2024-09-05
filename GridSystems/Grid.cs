using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace GridSystems
{
  public class Grid
  {
    #region// Enums

    #endregion

    #region// Fields
    public List<Room> Rooms;
    #endregion

    #region// Properties
    public int Width { get; set; }
    public int Height { get; set; }
    public Cell[,] Cells;
    #endregion

    #region// Constructors
    public Grid(int width, int height)
    {
      Rooms = new List<Room>();
      Width = width;
      Height = height;
      Cells = new Cell[Width, Height];
      Generate();
    }
    #endregion

    #region// Methods
    public void Generate()
    {
      for (int x = 0; x < Width; x++)
      {
        for (int y = 0; y < Height; y++)
        {
          Cells[x,y] = new Cell(x, y, 1);
        }
      }
    }
    public void Scramble()
    {
      for (int x = 0; x < Width; x++)
      {
        for (int y = 0; y < Height; y++)
        {
          Cells[x, y].State = RandomGenerator.NextInt(0, 1);
          Cells[x, y].IsWalkable = Cells[x, y].State > 0;
        }
      }
    }
    public Cell GetCell(int x, int y)
    {
      return Cells[x, y];
    }
    public void SetCell(int x, int y, Cell cell)
    {
      Cells[x, y] = cell;
    }
    public void SwapCell(int x, int y, int xx, int yy)
    {
      Cell cell = Cells[x, y];
      Cells[x, y] = Cells[xx, yy];
      Cells[xx, yy] = cell;
    }
    public static Cell[,] DeepCopyCells(Cell[,] trueGrid)
    {
      int width = trueGrid.GetLength(0);
      int height = trueGrid.GetLength(1);

      Cell[,] gridClone = new Cell[width, height];
      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          gridClone[x, y] = trueGrid[x, y].Copy();
        }
      }
      return gridClone;
    }
    public static void IdentifyRooms(Grid grid)
    {
      int width = grid.Width;
      int height = grid.Height;
      int roomID = 0;

      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          Cell cell = grid.GetCell(x, y);
          if (cell != null && cell.RoomID == -1 && cell.State > 0)
          {
            Room newRoom = new Room(roomID); // Create a new Room instance
            grid.Rooms.Add(newRoom); // Add the new room to the Rooms list
            FloodFill.Fill(grid, x, y, roomID, cell.State);
            roomID++;
          }
        }
      }
    }
    public int[] FindOpenSpot()
    {
      int[] position = new int[2];

      // Find room with sufficient size
      int roomIndex = RandomGenerator.NextInt(0, Rooms.Count - 1);
      while (Rooms[roomIndex].Cells.Count < 4)
      {
        roomIndex = RandomGenerator.NextInt(0, Rooms.Count - 1);
      }

      int cellIndex = RandomGenerator.NextInt(0, Rooms[roomIndex].Cells.Count - 1);
      position[0] = Rooms[roomIndex].Cells[cellIndex].X;
      position[1] = Rooms[roomIndex].Cells[cellIndex].Y;

      Console.WriteLine(position);
      return position;
    }
    public bool IsInsideGrid(int x, int y)
    {
      if (x < 0 || x >= Width || y < 0 || y >= Height) return false;
      else return true;
    }
    #endregion
  }
}
