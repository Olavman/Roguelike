using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystems
{
  public class RunedDaggerInventoryItem : BaseInventoryItem
  {
    public RunedDaggerInventoryItem(char ID, int quantity, CURSED_STATUS cursedStatus) : base (ID, quantity, cursedStatus)
    {
      PrefixName = "Runed dagger";
      SuffixName = "of testing";
    }
  }
}
