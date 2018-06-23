using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace 委托同步异步
{
    public delegate int AddHandler(int a, int b);

    class Program
    {
       
        public class 加法类
        {
            public static int Add(int a, int b)
            {
                Console.WriteLine("开始计算：" + a + "+" + b);
                Thread.Sleep(3000); //模拟该方法运行三秒
                Console.WriteLine("计算完成！");
                return a + b;
            }
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("===== 1同步调用 SyncInvokeTest =====");
            //AddHandler handler = new AddHandler(加法类.Add);
            //int result = handler.Invoke(1, 2);

            //Console.WriteLine("1继续做别的事情。。。");
            //Console.WriteLine(result);
            //Console.ReadKey();

            //Console.WriteLine("===== 2异步调用 AsyncInvokeTest =====");
            //AddHandler handler2 = new AddHandler(加法类.Add);
            ////IAsyncResult: 异步操作接口(interface)
            ////BeginInvoke: 委托(delegate)的一个异步方法的开始
            //IAsyncResult result2 = handler2.BeginInvoke(1, 2, null, null);
            //Console.WriteLine("2继续做别的事情。。。");
            ////异步操作返回
            //Console.WriteLine(handler2.EndInvoke(result2));
            //Console.ReadKey();


            ////3.BeginXXX和EndXXXX方法 使用代理的BeginInvoke 和 EndInvoke  BeginInvoke使用IAysnResult为参数
            //Console.WriteLine("===== 3异步回调第一种写法 AsyncInvokeTest =====");
            //AddHandler handler3 = new AddHandler(加法类.Add);
            ////异步操作接口(注意BeginInvoke方法的不同！)
            //IAsyncResult result3 = handler3.BeginInvoke(1, 2, new AsyncCallback(回调函数), "AsycState:OK");
            //Console.WriteLine("3继续做别的事情。。。");
            //Console.ReadKey();

            //Console.WriteLine("===== 3异步回调第二种写法（用lambda和Func） AsyncInvokeTest =====");//Func 4.0后可以是使用
            //Func<int, int, int> func1 = (a, b) =>
            //{
            //    return 加法类.Add(a, b);
            //};//此步相当于new AddHandler的代理 
            //func1.BeginInvoke(5, 5, ar =>
            //{
            //    int res = func1.EndInvoke(ar);
            //    Console.WriteLine("计算结果=" + res);
            //}, "AsycState:OK");
            //Console.WriteLine("4继续做别的事情。。。");
            //Console.ReadKey();

            Console.WriteLine("===== 5基于事件的异步模式 =====");//Func 4.0后可以是使用
            WebClient webclient = new WebClient();
            Uri url=new Uri("http://192.168.1.103:54999/Test.aspx");
            webclient.DownloadStringCompleted += (send1, e1) =>
            {
                string res = e1.Result;
                Console.WriteLine(res);
            };
           //上面的lambda相当月这句 webclient.DownloadStringCompleted += DownloadStringCompletedHaddler;
            webclient.DownloadStringAsync(url);//先明确事件要做什么 在调用异步执行
            Console.WriteLine("5继续做别的事情。。。");


            Console.WriteLine("===== 6基于任务Task的异步模式 =====");//使用async和await
            DownloadStringTaskAsyncHaddler(url);
            Console.WriteLine("6继续做别的事情。。。");


            Console.ReadKey();
        }
        static void 回调函数(IAsyncResult result3)
        {
            //result 是“加法类.Add()方法”的返回值
            //AsyncResult 是IAsyncResult接口的一个实现类，引用空间：System.Runtime.Remoting.Messaging
            //AsyncDelegate 属性可以强制转换为用户定义的委托的实际类。
            AddHandler handler = (AddHandler)((AsyncResult)result3).AsyncDelegate;
            Console.WriteLine(handler.EndInvoke(result3));
            Console.WriteLine(result3.AsyncState);
        }
        /* 问题：
        （1）int result = handler.Invoke(1,2);
        为什么Invoke的参数和返回值和AddHandler委托是一样的呢？
        答：Invoke方法的参数很简单，一个委托，一个参数表(可选)，而Invoke方法的主要功能就是帮助你在UI线程上调用委托所指定的方法。Invoke方法首先检查发出调用的线程(即当前线程)是不是UI线程，如果是，直接执行委托指向的方法，如果不是，它将切换到UI线程，然后执行委托指向的方法。不管当前线程是不是UI线程，Invoke都阻塞直到委托指向的方法执行完毕，然后切换回发出调用的线程(如果需要的话)，返回。
        所以Invoke方法的参数和返回值和调用他的委托应该是一致的。
        （2）IAsyncResult result = handler.BeginInvoke(1,2,null,null);
        BeginInvoke : 开始一个异步的请求,调用线程池中一个线程来执行，
        返回IAsyncResult 对象(异步的核心). IAsyncResult 简单的说,他存储异步操作的状态信息的一个接口,也可以用他来结束当前异步。
        注意: BeginInvoke和EndInvoke必须成对调用.即使不需要返回值，但EndInvoke还是必须调用，否则可能会造成内存泄漏。
        （3）IAsyncResult.AsyncState 属性：
        获取用户定义的对象，它限定或包含关于异步操作的信息。
         */

        private static void DownloadStringCompletedHaddler(object sender, DownloadStringCompletedEventArgs e)
        {
            Console.WriteLine(e.Result); 
        }
        //async用于修饰方法  方法内部使用 await(可有多个)TaskAsync方法返回Task<T> T为返回值类型 
        private async static void DownloadStringTaskAsyncHaddler(Uri url) 
        {
            WebClient webclient=new WebClient();
            string res = await webclient.DownloadStringTaskAsync(url);
            Console.WriteLine("---------6--------");
            Console.WriteLine(res);
            byte[] buff = await webclient.DownloadDataTaskAsync(url);
            Console.WriteLine("---------7--------");
            Console.WriteLine( Encoding.Default.GetString(buff));
        }
    }
}
