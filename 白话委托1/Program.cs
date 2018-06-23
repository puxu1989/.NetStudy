using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace 白话委托1
{
    class Program
    {
        //声明一个委托，其实就是个“命令”
        public delegate void BugTicketEventHandler();
        System.AsyncCallback _asyncCallback = null;//系统的
        static void Main(string[] args)
        {
            //这里就是具体阐述这个命令是干什么的，本例是MrZhang.BuyTicket“小张买车票”
            BugTicketEventHandler myDelegate = new BugTicketEventHandler(MrZhang.BuyTicket);
            
            myDelegate += MrZhang.BuyMovieTicket;
            //这时这个委托就相当于要做2件事情，先是买车票，再是买电影票拉！
            
           
            myDelegate(); //开始干事 这时候委托被附上了具体的方法
            //myDelegate.BeginInvoke(_asyncCallback, null);
            Console.ReadKey();
        }
        void inti() {
            _asyncCallback = new AsyncCallback(_onSent);
        }
        void _onSent(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            BugTicketEventHandler caller = (BugTicketEventHandler)result.AsyncDelegate;
            caller.EndInvoke(ar);
            Console.WriteLine("做完了");
            Console.ReadKey();
        }
    }
}
