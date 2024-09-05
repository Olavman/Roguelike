using GridSystems;
using Utility;
using InventorySystems;
using InputHandling;

namespace Entities
{
  public class Player : Entity
  {
    #region// Enums

    #endregion

    #region// Fields

    #endregion

    #region// Properties
    public bool Acting { get; set; } // Bool to tell if its the players turn
    public string Name { get; set; }
    public string Role { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Cash { get; set; }
    public int Experience { get; set; }
    public int Level { get; set; }
    public int Hunger { get; set; }
    public string Effects { get; set; }

    public Inventory<BaseInventoryItem> playerInventory;
    public int AttackPower { get; set; }
    public int Defense { get; set; }
    #endregion

    #region// Constructors
    public Player(string name, string role, int x, int y, char symbol, int viewDistance, int speed) : base(x, y, symbol, viewDistance, speed)
    {
      Acting = false;

      playerInventory = new Inventory<BaseInventoryItem>(52);
      Name = name;
      Role = role;

      InitializeStats();
      PickUpItem();


    }
    #endregion

    #region// Methods
    public void InitializeStats()
    {
      Level = 1;

      MaxHealth = 100;
      Health = MaxHealth;
      MaxMana = 10;
      Mana = MaxMana;

      Strength = 3;
      Dexterity = 3;
      Intelligence = 3;
      Wisdom = 3;

      AttackPower = 10;
      Defense = 5;
    }
    public int GetExpForNextLevel()
    {
      int requiredExp = Level*Level + 10;
      return requiredExp;
    }

    public override void Act(int currentTurn)
    {
      //Entity takes action (move, attack, etc.)
      //For simplicity, we'll just call the existing Move method as an example action
      //This should be replaced or extended with acutal game logic for different entities
      //Move(dx, dy, grid); //dx and dy would be determined by the entity's AI or player input
      

      //Calculate the next action turn based on the entity's speed
      CalculateTurnsUntilNextAction(currentTurn);
    }

    public void DirectionToMove(char direction, Grid grid)
    {
      int dx = 0;
      int dy = 0;

      switch (direction)
      {
        case 'w': //up
          dx = 0;
          dy = -1;
          break;
        case 's': //down
          dx = 0;
          dy = 1;
          break;
        case 'a': //left
          dx = -1;
          dy = 0;
          break;
        case 'd': //right
          dx = 1;
          dy = 0;
          break;
      }
      Acting = !Move(dx, dy, grid);
    }

    public override bool Move(int dx, int dy, Grid grid)
    {
      if (IsAllowedToMove(dx, dy, grid))
      {
        X += dx;
        Y += dy;
      }
      else
      {
        Cell cell = grid.GetCell(X+dx, Y+dy);
        if (cell.State == 0) 
        {
          MessagePublisher.Publish("You obvisously cannot walk trough a wall.");
          return false;
        }
      }

      return true;
    }
    public void Attack()
    {

    }
    public void Interract()
    {

    }
    public void PickUpItem()
    {
      //testing custom added items
      BaseInventoryItem item = new RunedDaggerInventoryItem(playerInventory.GenerateID(), 2, CURSED_STATUS.BLESSED);
      //BaseInventoryItem item2 = new RunedDaggerInventoryItem(playerInventory.GenerateID(), 5, CURSED_STATUS.BLESSED);
      BaseInventoryItem item3 = new RunedDaggerInventoryItem(playerInventory.GenerateID(), 1, CURSED_STATUS.UNCURSED);
      BaseInventoryItem item4 = new RunedDaggerInventoryItem(playerInventory.GenerateID(), 1, CURSED_STATUS.CURSED);
      playerInventory.AddItem(item);
      //playerInventory.AddItem(item2);
      playerInventory.AddItem(item3);
      playerInventory.AddItem(item4);
    }
    public void UseItem()
    {

    }

    #endregion
  }
}
