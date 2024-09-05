using GridSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Entities
{
  public class Entity
  {
    #region// Enums

    #endregion

    #region// Fields

    #endregion

    #region// Properties
    public int X {  get; set; }
    public int Y { get; set; }
    public int ViewDistance { get; set; }
    public char Symbol { get; protected set; }
    public ConsoleColor Color { get; set; }
    public int Speed { get; set; } //Higher values mean more frequent turns
    protected int nextActionTurn; //The next game turn this entity is allowed to act
    #endregion

    #region// Constructors
    public Entity (int x, int y, char symbol, int viewDistance, int speed)
    {
      X = x;
      Y = y;
      Symbol = symbol;
      ViewDistance = viewDistance;
      Speed = speed;
      nextActionTurn = 0;
    }
    #endregion

    #region// Methods
    public virtual void Act(int currentTurn)
    {
      //Entity takes action (move, attack, etc.)
      //For simplicity, we'll just call the existing Move method as an example action
      //This should be replaced or extended with acutal game logic for different entities
      //Move(dx, dy, grid); //dx and dy would be determined by the entity's AI or player input
    }

    public bool CanAct(int currentTurn)
    {
      if (currentTurn >= nextActionTurn)
      {
        return true;
      }
      return false;
    }

    protected virtual void CalculateTurnsUntilNextAction(int currentTurn)
    {
      //This method calculates how many turns until the entity can act again based on its speed
      //The calculation can be adjusted based on your game's design
      //For example, a simple inversion might be a fixed value minus the the entity's speed
      //Ensure the result is at least 1 to prevent entities from acting multiple times in a turn
      const int baseTurns = 10; //Arbitrary base for calculation
      nextActionTurn =  Math.Max(1, currentTurn + baseTurns - Speed);
    }
    public virtual bool Move (int dx, int dy, Grid grid)
    {
      if (IsAllowedToMove(dx, dy, grid))
      {
        X += dx;
        Y += dy;
      }

      return true;
    }

    public virtual bool IsAllowedToMove(int dx, int dy, Grid grid)
    {
      int x = X + dx;
      int y = Y + dy;
      if (!grid.IsInsideGrid(x, y) || !grid.GetCell(x, y).IsWalkable) return false;
      return true;
    }

    public virtual char Draw()
    {
      return Symbol;
    }
    #endregion
  }
}
