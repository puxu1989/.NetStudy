using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lambda相关
{
    /// <summary>
    /// C# 3.0之后 运算符 => 左边列出需要的参数 右边为表达式的方法实现代码块
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //1
            Func<string> func0=()=>//=(parm)
            {              
                return "1只有返回值无参表达式";
            };
            Console.WriteLine(func0.Invoke());
            //2 一个参数 只写参数名parm
            string middle = "中间部分";
            Func<string, string> func = parm =>//=(parm)
            {
                parm += middle;
                parm += "最后部分";
                return parm;
            };
            Console.WriteLine(func.Invoke("2一个参数第一部分"));


            //3 两个参数
            string middle2 = "中间部分";
            Func<string,string, string> func2 = (parm,s) =>
            {
                parm += middle2;
                parm += "最后部分";
                parm += s;
                return parm;
            };
            Console.WriteLine(func2.Invoke("3两个参数第一部分", "结尾了"));
            //4
            Func<double, double, double> func4 = (x, y) => x * y;
            Console.WriteLine("4只有一条语句的不用花括号{}返回值="+func4(4,4)+"  多条必须要用{}");
            //5闭包原则
            int value = 5;
            Func<int, int> func5 = x => x + value;
            value = 10;
            Console.WriteLine("5闭包原则"+func5(5));

            Console.ReadKey();
        }
    }
}
