using Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridSystems;
using Entities;
using Utility;

namespace Roguelike
{
  public class ConsoleUI
  {
    private AsciiRenderer asciiRenderer;
    private int mapWidth;
    private int mapHeight;
    private int screenWidth;
    private int screenHeight;
    public const int MessageAreaHeight = 4; // Lines reserved for messages
    public const int StatsAreaHeight = 5; // Lines reserved for messages
    private Queue<string> messageBuffer = new Queue<string>();

    public ConsoleUI (int screenWidth, int screenHeight, int mapWidth, int mapHeight)
    {
      this.asciiRenderer = new AsciiRenderer (mapWidth, mapHeight);
      this.mapWidth = mapWidth;
      this.mapHeight = mapHeight;
      this.screenWidth = screenWidth;
      this.screenHeight = screenHeight;

      Console.CursorVisible = false;
      Console.SetWindowSize(screenWidth, screenHeight);
      Console.SetBufferSize(screenWidth, screenHeight);

      MessagePublisher.OnMessagePublished += AddMessage;
    }
    
    public void RenderGame (Grid grid, List<Entity> entities, HashSet<Cell> visibleCells, Player player)
    {
      //if (Console.WindowHeight != screenHeight || Console.WindowWidth != screenWidth)
      //{
      //  Console.Clear();
      //}

      RenderMessages();
      asciiRenderer.RenderMap(grid, entities, visibleCells, MessageAreaHeight);
      RenderInventory(player);
      RenderStats(player);
    }
    private void RenderInventory(Player player)
    {
      int inventoryWidth = Console.WindowWidth - mapWidth; // Calculate the width of the inventory
      int inventoryHeight = Console.WindowHeight - StatsAreaHeight - MessageAreaHeight;

      // Check if there's enough space to render the inventory
      if (inventoryWidth <= 0  || inventoryHeight <= 0)
      {
        return;
      }

      Console.SetCursorPosition(mapWidth, StatsAreaHeight);

      // Render inventory
      Console.WriteLine("Inventory:");
      int line = 0;
      foreach(var Item in player.playerInventory.items)
      {
        int index = player.playerInventory.SelectIndex;

        line++;
        Console.SetCursorPosition(mapWidth, StatsAreaHeight+line);
        if (player.playerInventory.items.IndexOf(Item) == index)
        {
          Console.Write(">"); //Selected item
        }
        else Console.Write(" "); //Not selected items

        Console.WriteLine($"{Item.GetInformation()}");
      }
    }
    private void RenderStats(Player player)
    {
      int statsPosition = Console.WindowHeight - StatsAreaHeight; // Places the stats on the bottom of the screen
      Console.SetCursorPosition(0, statsPosition);

      // Render stats
      Console.WriteLine($"{player.Name} the {player.Role}");
      Console.WriteLine($"Hp:{player.Health}/{player.MaxHealth} Mana:{player.Mana}/{player.MaxMana} Exp:{player.Experience}/{player.GetExpForNextLevel()} Lvl:{player.Level}");
      Console.WriteLine($"Str:{player.Strength} Dex:{player.Dexterity} Int:{player.Intelligence} Wis:{player.Wisdom}");
      Console.WriteLine($"$:{player.Cash} Armor:{player.Defense}");
      Console.Write($"Turn:{GameManager.TurnCounter} Effects:{player.Effects} Hunger:{player.Hunger}");
    }

    private void RenderMessages()
    {
      Console.SetCursorPosition(0, 0); // Starting at the top
      foreach (var message in  messageBuffer) 
      {
        Console.WriteLine(message.PadRight(Console.WindowWidth - 1));// Ensure each message only takes up one line
      }

      // Clear remaining lines if any
      for (int i = messageBuffer.Count; i < MessageAreaHeight; i++)
      {
        Console.WriteLine(new string(' ', Console.WindowWidth - 1));
      }
    }

    private void AddMessage(string message)
    {
      if (messageBuffer.Count == MessageAreaHeight)
      {
        messageBuffer.Dequeue(); // Remove the oldest message
      }
      messageBuffer.Enqueue(GameManager.TurnCounter +": " + message); // Add newest message
    }

  }
}
