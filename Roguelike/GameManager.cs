using Entities;
using System.Collections.Generic;
using System.Linq;
using InputHandling;
using GridSystems;
using System;

namespace Roguelike
{
  public enum GameState
  {
    Exploration,
    Inventory,
    Dialogue
  }

  public class GameManager
  {
    private InputHandler inputHandler;
    private Player player;
    private Grid grid;
    public GameState gameState;
    public bool isRunning;
    public static int TurnCounter { get; private set; }

    public GameManager(InputHandler inputHandler, Player player, Grid grid) 
    {
      this.inputHandler = inputHandler;
      this.player = player;
      this.grid = grid;

      gameState = GameState.Exploration;
      isRunning = true;
      TurnCounter = 0;

      //Subscrive to input events
      this.inputHandler.OnCommandRecieved += HandleInput;
    }

    private void HandleInput(char command)
    {
      //If game state allows player to move
      switch (gameState)
      {
        case GameState.Exploration:
          switch (command)
          {
            case 'w': // up
            case 's': // down
            case 'a': // left
            case 'd': // right
              player.DirectionToMove(command, grid);
              break;
            case 'i': // open inventory
              gameState = GameState.Inventory;
              break;
          }
          break;
        case GameState.Inventory:
          switch (command)
          {
            case 'w': // up
              player.playerInventory.DecreaseSelectIndex();
              break;
            case 's': // down
              player.playerInventory.IncreaseSelectIndex();
              break;
            case 'i': // close inventory
              gameState = GameState.Exploration;
              break;
          }
          break;
      }
    }

    public void NextTurn(List<Entity> entities)
    {
        TurnCounter++;
        HandleEntityTurns(entities);
    }

    private void HandleEntityTurns(List<Entity> entities)
    {
      foreach (Entity entity in entities.OrderBy(e => e.Speed))
      {
        if (entity.CanAct(TurnCounter))
        {
          //MessagePublisher.Publish(entity.Symbol + "'s turn " + TurnCounter); //Prints a message, saying whos turn it is
          if (entity is Player)
          {
            player.Acting = true;
            while (player.Acting)
            {
              inputHandler.ProcessInput();
            }
          }
          entity.Act(TurnCounter);
        }
      }
    }
  }
}
