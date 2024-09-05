using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridSystems;

namespace Algorithms
{
  public class CellularAutomata
  {
    #region// Enums

    #endregion

    #region// Fields
    private CellularAutomataRules rules;
    private Cell[,] grid;
    #endregion

    #region// Properties
    public CellularAutomata(CellularAutomataRules rules, int width, int height)
    {
      this.rules = rules;
      this.grid = new Cell[width, height];
      InitializeGrid();
    }

    #endregion

    #region// Constructors

    #endregion

    #region// Methods
    private void InitializeGrid()
    {
    }

    public void ApplyRules()
    {

    }
    public void RunAutomata()
    {

    }
    public Cell[,] GetGrid()
    {
      return grid;
    }

    #endregion
  }
}
