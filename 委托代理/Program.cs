using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 委托代理
{
    //定义委托，delegate 返回值 委托名 入参  委托：方法的签名
    public delegate void GreetingDelegate(string name);
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
        //注意此方法，它接受一个GreetingDelegate类型的参数，该参数是返回值为空，参数为string类型的方法
        private static void GreetPeople(string name, GreetingDelegate MakeGreeting)
        {
            MakeGreeting(name);
        }
        static void Main(string[] args)
        {
            //总结：我们一般的方法是传入数据 而委托是将其作为参数传给方法。委托是一个类，它定义了方法的类型，使得可以将方法当作另一个方法的参数来进行传递，这种将方法动态地赋给参数的做法，可以避免在程序中大量使用If-Else(Switch)语句，同时使得程序具有更好的可扩展性。
            //1.使用
            GreetingDelegate delegate1 = new GreetingDelegate(EnglishGreeting);
            delegate1 += ChineseGreeting; // 给此委托变量再绑定一个方法
            // 将先后调用 EnglishGreeting 与 ChineseGreeting 方法
            GreetPeople("Jimmy Zhang", delegate1);
            Console.WriteLine();
            delegate1 -= EnglishGreeting; //取消对EnglishGreeting方法的绑定
            // 将仅调用 ChineseGreeting
            GreetPeople("张子阳", delegate1);
            Console.ReadKey();
            //总结：使用委托可以将多个方法绑定到同一个委托变量，当调用此变量时(这里用“调用”这个词，是因为此变量代表一个方法)，可以依次调用所有绑定的方法。
        }
        
   }
}
