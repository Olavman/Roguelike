using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GridSystems;
using CellularAutomataSystems;

namespace LevelGenerationSystems
{
  public class LevelGenerator
  {
    public List<Grid> grids;
    public int CurrentLevel {  get; set; }

    public LevelGenerator(int currentLevel) 
    {
      grids = new List<Grid>();
      CurrentLevel = currentLevel;
    }

    public void GenerateNewLevel(int width, int height)
    {
      int level = grids.Count; // Level to generate
      grids.Add(new Grid(width, height));
    }

    public void GenerateCave(int width, int height, int iterations)
    {
      int level = grids.Count; // Level to generate
      grids.Add(new Grid(width, height)); // Initialize a new level

      grids[level].Scramble(); // Randomize level
      CaveGenerationRule caveRule = new CaveGenerationRule();
      CellularAutomata.ApplySpesificRule(grids[level], caveRule, iterations);
      Grid.IdentifyRooms(grids[level]);
      RoomConnector.ConnectRooms(grids[level]);
    }
  }
}
