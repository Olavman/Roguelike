using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystems
{
  public class Cell
  {
    #region// Enums

    #endregion

    #region// Fields

    #endregion

    #region// Properties
    public int State {  get; set; }
    public bool IsWalkable { get; set; }
    public int X {  get; set; }
    public int Y { get; set; }
    public int RoomID {  get; set; }
    #endregion

    #region// Constructors
    public Cell(int x, int y, int state)
    {
      X = x;
      Y = y;
      State = state;
      RoomID = -1;
      IsWalkable = state > 0;
    }
    #endregion

    #region// Methods
    public char ToChar()
    {
      // Return ASCII character or symbol based on the cell's content
      switch (State)
      {
        case 0:
          return '#';
        case 1:
          return '.';
        case 2:
          return '^';
        case 3:
          return '+';
        default:
          return '?';
      }
    }

    public bool IsRoom()
    {
      return RoomID != -1;
    }

    public Cell Copy()
    {
      Cell newCell = new Cell(this.X, this.Y, this.State)
      {
        RoomID = this.RoomID,
        IsWalkable = this.IsWalkable
      };
      return newCell;
    }
    #endregion
  }
}
