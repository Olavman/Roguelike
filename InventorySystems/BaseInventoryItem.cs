using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystems
{
  public enum CURSED_STATUS
  {
    UNIDENTIFIED,
    CURSED,
    UNCURSED,
    BLESSED
  }

  public abstract class BaseInventoryItem
  {
    public int Quantity { get; set; }
    public string PrefixName { get; set; }
    public string SuffixName { get; set; }
    public char ID { get; set; }
    public CURSED_STATUS CursedStatus { get; set; }

    public BaseInventoryItem(char ID, int quantity, CURSED_STATUS cursedStatus)
    {
      this.ID = ID;
      Quantity = quantity;
      CursedStatus = cursedStatus;
    }
    public string GetInformation()
    {
      string info = ID.ToString() + " - ";

      if (Quantity == 1)
      {
        switch (CursedStatus)
        {
          case CURSED_STATUS.CURSED:
            info += "A cursed ";
            break;
          case CURSED_STATUS.UNCURSED:
            info += "An uncursed ";
            break;
          case CURSED_STATUS.BLESSED:
            info += "A blessed ";
            break;
          default:
            break;
        }
        if (CursedStatus == CURSED_STATUS.UNIDENTIFIED)
        {
          if (IsVowel(PrefixName[0]))
          {
            info += "An ";
          }
          else
          {
            info += "A ";
          }
        }
      }
      else
      {
        info += Quantity.ToString() + " ";
        switch (CursedStatus)
        {
          case CURSED_STATUS.CURSED:
            info += "cursed ";
            break;
          case CURSED_STATUS.UNCURSED:
            info += "uncursed ";
            break;
          case CURSED_STATUS.BLESSED:
            info += "blessed ";
            break;
          default:
            break;
        }
      }

      info += PrefixName;

      if (Quantity > 1)
      {
        info += "s ";
      }
      else
      {
        info += " ";
      }

      info += SuffixName;

      return info;
    }

    private bool IsVowel(char c)
    {
      bool vowel = "aeiouyAEIOUY".IndexOf(c) >= 0;
      return vowel;
    }
  }
}
