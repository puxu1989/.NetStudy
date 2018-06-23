using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    class Program
    {
        private static void EnglishGreeting(string name)
        {
            Console.WriteLine("Morning, " + name);
        }
        private static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好, " + name);
        }
        static void Main(string[] args)
        {
            GreetingManager gm = new GreetingManager();
            gm.GreetPeople("Jimmy Zhang", EnglishGreeting);
            gm.GreetPeople("张子阳", ChineseGreeting);
            Console.ReadKey();
        }
    }
}
