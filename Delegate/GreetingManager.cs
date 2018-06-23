using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public delegate void GreetingDelegateEventHandler(string name);
   public class GreetingManager
    {
       public void GreetPeople(string name, GreetingDelegateEventHandler MakeGreeting)
       {
           MakeGreeting(name);
       }
    }
}
