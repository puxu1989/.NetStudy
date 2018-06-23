using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 白话委托事件2
{
    public class MrMing
    {
        //对事件感兴趣的事情，这里指对出版社的书感兴趣
        public static void Receive1()
        {
            Console.WriteLine("嘎嘎，我已经收到最新一期的《火影忍者》啦！！");
        }
    }
    //Subscriber 订阅者，悲情人物小张
    public class MrZhang
    {
        //对事件感兴趣的事情
        public static void Receive2()
        {
            Console.WriteLine("幼稚，这么大了，还看《火影忍者》，SB小明！");
        }
    }
    class Program
    {
       
        static void Main(string[] args)
        {
            //实例化一个出版社
            Publisher publisher = new Publisher();

            //给这个出火影忍者的事件注册感兴趣的订阅者，此例中是小明
            publisher.OnPublish += new Publisher.PublishEventHander(MrMing.Receive1);
            //另一种事件注册方式
            //publisher.OnPublish += MrMing.Receive;
            publisher.OnPublish += new Publisher.PublishEventHander(MrZhang.Receive2);
            //publisher.OnPublish -= new Publisher.PublishEventHander(MrMing.Receive);
            //发布者在这里触发出版火影忍者的事件
            publisher.issue();

            Console.ReadKey();
        }
    }
}
