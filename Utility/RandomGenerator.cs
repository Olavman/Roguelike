using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
  public static class RandomGenerator
  {
    private static Random _random = new Random();
    public static int NextInt(int minValue,  int maxValue)
    {
      return _random.Next(minValue, maxValue+1);
    }

    public static double NextDouble()
    {
      return _random.NextDouble();
    }

    public static bool NextBool()
    {
      return _random.Next(2) == 0;
    }

    // Method to return a random elementa from an array
    public static T Choose<T>(T[] array)
    {
      int index = _random.Next(array.Length);
      return array[index];
    }
  }
}
