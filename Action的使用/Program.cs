using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action的使用
{
   public delegate void DisplayMessage(string message);//区别Action定义的委托


    class Program
    {
        static void Main(string[] args)
        {
            //1.
            DisplayMessage messageTarget = new DisplayMessage(ShowWindowsMessage);//创建委对象 同时指定委托的任务（命令）//如果使用事件就用+=
            //写法2 也可以直接给委托对象直接指定任务
            messageTarget("委托的Hello, World!"); //委托对象执行任务，同时传入委托定义规定的参数  
            //2.
            Action<string> actionMsgTarget=ActionShowMsg;
            actionMsgTarget("Action的Hello World!");

            //3.Action作为参数
            Test<string>(Action, "s-Hello World!");
            Test<int>(Action, 1000);
            Test<string>(p => { Console.WriteLine("{0}", p); }, "Lambda_Hello World");//使用Lambda表达式定义委托
            

            //4
            Action<string, string> BookAction = new Action<string, string>(Book);//==Book
            BookAction("百年孤独", "北京大书店");
            Console.ReadKey();
        }
        private static void ShowWindowsMessage(string message)
        {
            Console.WriteLine(message);
        }
        static void ActionShowMsg(string msg) 
        {
            Console.WriteLine(msg);
        }
        public static void Test<T>(Action<T> action, T intp)//Action为参数传递
        {
            action(intp);
        }
        private static void Action(string s)//Action无返回值
        {
            Console.WriteLine(s);
        }
        private static void Action(int s)
        {
            Console.WriteLine(s);
        }

        //4例子
        public static void Book(string BookName,string ChangJia)
        {
            Console.WriteLine("我是买书的是:{0}来自{1}",BookName,ChangJia);
        }
    }
}
