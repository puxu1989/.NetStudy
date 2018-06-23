using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func相关
{
    //Func 解释 封装一个不定具有参数（也许没有）但却返回 TResult 参数指定的类型值的方法。 最多16个in参数
    /// <summary>
    /// 总结:
    /// 1：Action用于没有返回值的方法（参数可以根据自己情况进行传递）
    /// 2：Func恰恰相反用于有返回值的方法（同样参数根据自己情况情况）
    /// 3：记住无返回就用action，有返回就用Func
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //1无参返回值
            Func<string> RetBook = FuncBook;//=new Func<string>(FuncBook); 这里的string是返回类型  委托一种事先定义好的方法 另一种是匿名方或者lambda表达式
            Console.WriteLine(RetBook.Invoke());//同步调用=RetBook();
         
            //2有参有返回值
            Func<string, string> RetBook2 = new Func<string, string>(FuncBook);//第一个string是入参类型 第二个string是返回值类型
            Console.WriteLine(RetBook2("2快递员送书来了"));

            //3作为参数传递
            Func<string> funcValue = delegate()//匿名方法代码块 注意匿名方法的使用  代码减少并没有减少执行速度 编译器内部指定了一个我们不知道的方法 都用lambda代替
            {
                int x = 3;
                return "我是即将传递的值"+x;
            };
            DisPlayValue(funcValue);
            Console.WriteLine("并没有异步3");

            //4lambda使用
            Func<string> lamFun = new Func<string>(() =>//()=>{ //todo }lambda代码块 只要有委托类型参数的地方都可以用lambda
            {
                int x = 4;
                return "4lambdaFun"+x;
            });
            Console.WriteLine(lamFun());


            Console.ReadKey();
        }

        public static string FuncBook()
        {
            return "1送书来了";
        }
        public static string FuncBook(string BookName)
        {
            return BookName;
        }
        /// <summary>
        /// 3
        /// </summary>
        /// <param name="func"></param>
        private static void DisPlayValue(Func<string> infuncValue)
        {
            string value = infuncValue.Invoke();
            Console.WriteLine("我在测试一下传过来值：{0}", value);
        }
    }
}
