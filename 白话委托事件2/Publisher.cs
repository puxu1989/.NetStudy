using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 白话委托事件2
{
    //发布者（Publiser)
    public class Publisher
    {
        //声明一个出版的委托
        public delegate void PublishEventHander();
        //在委托的机制下我们建立一个出版事件
        public event PublishEventHander OnPublish;
        //事件必须要在方法里去触发，出版社发布新书方法
        public void issue()
        {
            //如果有人注册了这个事件，也就是这个事件不是空
            if (OnPublish != null)
            {
                Console.WriteLine("最新一期的《火影忍者》今天出版哦！");//事件发布一次 
                OnPublish();//通知订阅者多次
            }
        }
    }
}
