using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystems
{
  public class Inventory<T> where T : BaseInventoryItem
  {
    public List<T> items;
    public int capacity;
    public int SelectIndex { get; set; }

    public Inventory(int capacity)
    {
      this.capacity = capacity;
      items = new List<T>();
    }

    public void IncreaseSelectIndex()
    {
      SelectIndex = Math.Max(0, Math.Min(items.Count-1, SelectIndex + 1));
      MessagePublisher.Publish("Increment " + SelectIndex);
    }

    public void DecreaseSelectIndex()
    {
      SelectIndex = Math.Max(0, SelectIndex-1);
      MessagePublisher.Publish("Decrement " + SelectIndex);
    }

    public bool AddItem(T item)
    {
      if (items.Count < capacity)
      {
        if (!CanStack(item))
        {
          items.Add(item); 
        }
        return true; // Item added successfully
      }
      else { return false; }
    }

    public bool RemoveItem(T item)
    {
      return items.Remove(item);
    }

    public bool HasItem(T item)
    {
      return items.Contains(item);
    }

    public bool CanStack (T item)
    {
      if (items.Count <= 0) return false;

      string nameSearch = item.PrefixName;

      // Find all items with the same name
      List<T> itemList = items.Where(item1 => item1.PrefixName == nameSearch).ToList();

      // Check if you have more than one of the item
      if (itemList.Count > 0)
      {
        foreach(var item1 in  itemList)
        {
          if (item1.CursedStatus == item.CursedStatus)
          {
            itemList[0].Quantity += item.Quantity; // Stack item
            return true;
          }
        }
      }
      return false;
    }

    public List<T> GetItems()
    {
      return new List<T>(items);
    }

    public char GenerateID()
    {
      return (char)(items.Count+65);
    }

    public int GetCapacity()
    {
      return capacity;
    }
  }
}
