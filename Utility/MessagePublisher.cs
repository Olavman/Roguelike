using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
  public class MessagePublisher
  {
    public static event Action<String> OnMessagePublished;
    public delegate void MessagePublished();

    public static event MessagePublished messagePublished;

    public static void Publish(String message)
    {
      OnMessagePublished?.Invoke(message);
      messagePublished?.Invoke();
    }
  }
}
