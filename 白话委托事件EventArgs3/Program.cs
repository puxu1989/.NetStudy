using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 白话委托事件EventArgs3
{
    public class PubEventArgs : EventArgs
    {
        public readonly string magazineName;
        public PubEventArgs(string magazineName)
        {
            this.magazineName = magazineName;
        }
    }
    //发布者（Publiser)
    public class Publisher
    {
        //声明一个出版的委托
        //这里多了一个参数sender,它所代表的就是Subject，也就是监视对象，本例中就是Publisher
        public delegate void PublishEventHander(object sender, PubEventArgs e);
        //在委托的机制下我们建立以个出版事件
        public event PublishEventHander Publish;

        //声明一个可重写的OnPublish的保护函数
        protected virtual void OnPublish(PubEventArgs e)
        {
            if (Publish != null)
            {
                //Sender = this，也就是Publisher
                this.Publish(this, e);
            }
        }

        //事件必须要在方法里去触发
        public void issue(string magazineName)
        {
            OnPublish(new PubEventArgs(magazineName));
        }
    }

    //Subscriber 订阅者
    public class MrMing
    {
        //对事件感兴趣的事情
        public static void Receive(object sender, PubEventArgs e)
        {
            Console.WriteLine("嘎嘎，我已经收到最新一期的《" + e.magazineName + "》啦！！");
        }
    }

    public class MrZhang
    {
        //对事件感兴趣的事情
        public static void Receive(object sender, PubEventArgs e)
        {
            Console.WriteLine("幼稚，这么大了，还看《火影忍者》，SB小明！");
            Console.WriteLine("这个我定的《" + e.magazineName + "》，哇哈哈！");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //实例化一个出版社
            Publisher publisher = new Publisher();

            Console.Write("请输入要发行的杂志：");
            string name = Console.ReadLine();

            if (name == "火影忍者")
            {
                //给这个出火影忍者的事件注册感兴趣的订阅者，此例中是小明
                publisher.Publish += new Publisher.PublishEventHander(MrMing.Receive);
                //发布者在这里触发出版火影忍者的事件
                publisher.issue("火影忍者");
            }
            else
            {
                //给这个出火影忍者的事件注册感兴趣的订阅者，此例中是小明[另一种事件注册方式]
                publisher.Publish += MrZhang.Receive;
                publisher.issue("环球日报");
            }
            Console.ReadKey();
        }
    }
}
