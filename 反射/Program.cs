using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace 反射
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(Person);
            MethodInfo methodInfo = t.GetMethod("Say");
            Person person = new Person();
            string word = "hello";
            Person p = null;
            object[] param = new object[] { word, p, 3 };
            int TestTimes = 10000; //测试次数，可自行调节看效果

            #region 快速反射
            try
            {
                Stopwatch watch1 = new Stopwatch();
                FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(methodInfo);
                watch1.Start();
                for (int i = 0; i < TestTimes; i++)
                {
                    fastInvoker(person, param);
                }
                watch1.Stop();
                Console.WriteLine(TestTimes.ToString() + " times invoked by FastInvoke: " + watch1.ElapsedMilliseconds + "ms");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("快速反射 错误:" + ex.Message);
            }
            #endregion
            #region 传统方式反射
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < TestTimes; i++)
                {
                    methodInfo.Invoke(person, param);
                }
                watch.Stop();
                Console.WriteLine(TestTimes.ToString() + " times invoked by Reflection: " + watch.ElapsedMilliseconds + "ms");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("传统方式反射 直接错误:" + ex.Message);
                Console.WriteLine("传统方式反射 内部错误:" + ex.InnerException.Message);
            }
            #endregion

          

            #region 直接调用
            try
            {
                Stopwatch watch2 = new Stopwatch();
                watch2.Start();
                for (int i = 0; i < TestTimes; i++)
                {
                    person.Say(ref word, out p, 3);
                }
                watch2.Stop();
                Console.WriteLine(TestTimes.ToString() + " times invoked by DirectCall: " + watch2.ElapsedMilliseconds + "ms");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("直接调用 错误:" + ex.Message);
            }
            #endregion
            
            Console.ReadLine();
        }
    }
    public class Person
    {
        public void Say(ref string word, out Person p, int avi)
        {
            word = "ttt" + avi.ToString();
            p = new Person();

            //throw new System.Exception("出错了哦");
        }
    }
}
