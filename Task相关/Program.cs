using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task相关
{

    class Program
    {

        static void Main(string[] args)
        {
            //Task是微软在.net framework 4.0发布的新的异步编程的利器，当然4.5新增了async、await，这儿我们先说Task相关。
            //在实际编程中，我们用的较多的是Task、Task.Factory.StarNew、Task.Run，接下来简单的表述下我的理解。
            Console.WriteLine("----------------------1同步调用");
            Console.WriteLine(SayHello("1:小明"));
            Console.ReadKey();
            Console.WriteLine("----------------------2异步调用");
            CallerSayHelloAsync("小明");
            Console.WriteLine("2继续做事");
            MultipleSayHelloAsync("小明", "小华");
            MultipleSayHelloAsyncMethodCombin("组合小明", "组合小华");
            Console.WriteLine("3继续做事");
            Console.ReadKey();
            Console.WriteLine("----------------------4转换异步调用");
            ConvertingAsync("转换小明");
            Console.WriteLine("4继续做事");
            Console.ReadKey();
        }
        /// <summary>
        /// 创建同步方法 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string SayHello(string name)
        {
            Task.Delay(3000).Wait();
            return "Hello:" + name;
        }
        /// <summary>
        /// 创建异步方法 返回Task<string> 方法名加上Async结尾 内部用Task.Run创建对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Task<string> SayHelloAsync(string name)
        {
            return Task.Run<string>(() => //Task.Run<>(Func<string> func)
            {
                return SayHello(name);
            });
        }
        /// <summary>
        /// 异步方法调用 使用async修饰,async只能用于返回Task或者void,IAsyncOperation await只能用于返回Task的方法
        /// </summary>
        /// <param name="name"></param>
        public async static void CallerSayHelloAsync(string name)
        {
            string sayReslut = await SayHelloAsync(name);
            Console.WriteLine("异步调用返回：" + sayReslut);
        }
        /// <summary>
        /// 依赖顺序调用
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        public async static void MultipleSayHelloAsync(string name1, string name2)
        {
            string sayReslut1 = await SayHelloAsync(name1);
            string sayReslut2 = await SayHelloAsync(name2);//等待sayReslut1执行完了再执行sayReslut2
            Console.WriteLine("依赖顺序 异步调用返回：" + sayReslut1 + " and " + sayReslut2);//多个await sayReslut1依赖于sayReslut2所以都要等待执行
        }
        /// <summary>
        /// 组合调用 如果一个异步await不依赖于领一个await 则不能使用await 而是把结果直接放给Task变量 在使用await Task.WhenAll 结果就快了
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        public async static void MultipleSayHelloAsyncMethodCombin(string name1, string name2)
        {
            Task<string> sayReslut1 = SayHelloAsync(name1);
            Task<string> sayReslut2 = SayHelloAsync(name2);
            //await Task.WhenAll(sayReslut1, sayReslut2);//方式1
            //Console.WriteLine("组合调用 异步调用返回：" + sayReslut1.Result + " and " + sayReslut2.Result);//取Task<string>对象的Result
            string[] s = await Task.WhenAll(sayReslut1, sayReslut2);//方式2 异步都返回同类型的就可以返回一个数组
            Console.WriteLine("组合调用 异步调用返回：" + s[0] + " and " + s[1]);//取Task<string>对象的Result
        }
        /// <summary>
        /// 转换异步模式  并不是所有的。net类都有任务异步模式Task<T>的封装  大多数还是BeginXXX EndXXX的方式进行异步的 
        /// 使用Task<string>.Factory.FromAsync()具体看重载
        /// </summary>
        public async static void ConvertingAsync(string name)
        {
            string s = await Task<string>.Factory.FromAsync(ConvertingBeginInvoke, ConvertingEndInvoke,name,"status");
            Console.WriteLine(s);
        }
        private static Func<string, string> convertFun = SayHello;
        private static IAsyncResult ConvertingBeginInvoke(string name, AsyncCallback callback, object status) 
        {
            return convertFun.BeginInvoke(name,callback,status);
        }
        private static string ConvertingEndInvoke(IAsyncResult ar)
        {
            return convertFun.EndInvoke(ar);
        }
    }
}
