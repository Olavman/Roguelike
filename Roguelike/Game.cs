using Entities;
using GridSystems;
using System.Collections.Generic;
using LevelGenerationSystems;
using InputHandling;
using Utility;

namespace Roguelike
{
  internal class Game
  {
    public LevelGenerator levels; //Generates an array of levels
    private Player player; //Player entity
    private List<Entity> entities; //List of entities
    private ConsoleUI consoleUI; //User interface
    private int mapWidth;
    private int mapHeight;
    private int screenWidth;
    private int screenHeight;
    private GameManager gameManager; //Handles game states
    private InputHandler inputHandler; //Sends events for key presses

    public Game()
    {
      InitializeGame();
      inputHandler = new InputHandler();
      gameManager = new GameManager(inputHandler, player, levels.grids[0]);

      MessagePublisher.messagePublished += RenderGrid; //Makes sure the grid renders whenever a message is being published
    }

    public void InitializeGame()
    {
      mapWidth = 150;
      mapHeight = 50;
      screenWidth = mapWidth + 50;
      screenHeight = mapHeight + 9;
      levels = new LevelGenerator(0);
      consoleUI = new ConsoleUI(screenWidth, screenHeight, mapWidth, mapHeight);
      entities = new List<Entity>();

      // Create a level
      levels.GenerateCave(mapWidth, mapHeight, 10);

      // Add the player to the entities list
      int[] position = levels.grids[0].FindOpenSpot();
      player = new Player("Olav", "wizard", position[0], position[1], '@', mapHeight / 2, 5);
      entities.Add(player);
    }

    public void Run()
    {
      // Simple game loop
      while (gameManager.isRunning)
      {
        RenderGrid();

        gameManager.NextTurn(entities);
      }
    }

    public void RenderGrid()
    {
      LineOfSight los = new LineOfSight(player.ViewDistance);
      HashSet<Cell> visibleCells;

      // Get list of visible cells
      visibleCells = los.GetVisibleCells(levels.grids[0], player.X, player.Y);

      // Render the grid
      consoleUI.RenderGame(levels.grids[levels.CurrentLevel], entities, visibleCells, player);
    }
  }
}
