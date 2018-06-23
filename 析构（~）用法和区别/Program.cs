using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MessageLengthEx = System.UInt32;

namespace 析构___用法和区别
{
    class First
    {
        ~First()
        {
            System.Diagnostics.Trace.WriteLine("First's destructor is called.");
        }
    }

    class Second : First
    {
        ~Second()
        {
            //Console.WriteLine("Second's destructor is called.");
            System.Diagnostics.Trace.WriteLine("Second's destructor is called.");
        }
    }

    class Third : Second
    {
       
        ~Third()
        {
            //Console.WriteLine("Third's destructor is called.");
            System.Diagnostics.Trace.WriteLine("Third's destructor is called.");
        }
        public Third()
        {
            byte[] _buffer = new byte[2048];
            int _sending=0;
            MessageLengthEx e = 1460;
           int rs= Interlocked.Add(ref _sending, 0);
            Console.WriteLine("Third's 构造函数 is called." + (int)e);
            Console.WriteLine("Test. _sending=" + _sending+" rs="+rs);
            int space = 0;
            int tt_wpos = 0 % _buffer.Length;
            int tt_spos = rs % _buffer.Length;
            if (tt_wpos >= tt_spos)
                space = _buffer.Length - tt_wpos + tt_spos - 1;
            else
                space = tt_spos - tt_wpos - 1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Third t = new Third();
            Console.ReadKey();
        }
    }
        /*析构函数与Dispose()方法的区别
        1. Dispose需要实现IDisposable接口。
        2. Dispose由开发人员代码调用，而析构函数由GC自动调用。
        3. Dispose方法应释放所有托管和非托管资源。而析构函数只应释放非托管资源。因为析构函数由GC来判断调用，当GC判断某个对象不再需要的时候，则调用其析构方法，这时候该对象中可能还包含有其他有用的托管资源。
        4. 通过系统GC频繁的调用析构方法来释放资源会降低系统性能，所以推荐显示调用Dispose方法。
        5. Dispose方法结尾处加上代码“GC.SuppressFinalize(this);”，即告诉GC不需要再调用该对象的析构方法，否则，GC仍会在判断该对象不再有用后调用其析构方法，虽然程序不会出错，但影响系统性能。
        6、析构函数 和 Dispose 释放的资源应该相同，这样即使类使用者在没有调用 Dispose 的情况下，资源也会在 Finalize 中得到释放。
        7、Finalize 不应为 public。
        8、有 Dispose 方法存在时，应该调用它，因为 Finalize 释放资源通常是很慢的
        */
}
