using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 白话委托事件银行取款通知
{
    class Program
    {
        //Obverser电子邮件，手机关心的对象e ,分别是邮件地址、手机号码、取款金额
        public class UserEventArgs : EventArgs
        {
            public readonly string emailAddress;
            public readonly string mobilePhone;
            public readonly string amount;
            public UserEventArgs(string emailAddress, string mobilePhone, string amount)
            {
                this.emailAddress = emailAddress;
                this.mobilePhone = mobilePhone;
                this.amount = amount;
            }
        }
        //发布者，也就是被监视的对象-银行账号
        class BankAccount
        {
            //声明一个处理银行交易的委托
            public delegate void ProcessTranEventHandler(object sender, UserEventArgs e);
            //声明一个事件
            public event ProcessTranEventHandler eProcessTran;

            public void Prcess(UserEventArgs e)
            {
                if (eProcessTran != null)
                {
                    eProcessTran(this, e);
                }
            }
        }
        //观察者Email
        class Email
        {
            public static void SendEmail(object sender, UserEventArgs e)
            {
                Console.WriteLine("向用户邮箱" + e.emailAddress + "发送邮件:您在" + System.DateTime.Now.ToString() + "取款金额为" + e.amount);
            }
        }

        //观察者手机
        class Mobile
        {
            public static void SendNotification(object sender, UserEventArgs e)
            {
                Console.WriteLine("向用户手机" + e.mobilePhone + "发送短信:您在" + System.DateTime.Now.ToString() + "取款金额为" + e.amount);
            }
        }
        //订阅系统，实现银行系统订阅几个Observer，实现与客户端的松耦合
        class SubscribSystem
        {
            public SubscribSystem(BankAccount bankAccount, UserEventArgs e)
            {
                //现在我们在银行账户订阅2个，分别是电子邮件和手机短信
                bankAccount.eProcessTran += new BankAccount.ProcessTranEventHandler(Email.SendEmail);
                bankAccount.eProcessTran += new BankAccount.ProcessTranEventHandler(Mobile.SendNotification);
                bankAccount.Prcess(e);
            }
        }

        static void Main(string[] args)
        {
            Console.Write("请输入您要取款的金额：");
            string amount = Console.ReadLine();
            Console.WriteLine("交易成功，请取磁卡。");
            //初始化e
            UserEventArgs user = new UserEventArgs("jinjiangbo2008@163.com", "18868789776", amount);

            //初始化订阅系统
            SubscribSystem subject = new SubscribSystem(new BankAccount(), user);
            Console.ReadKey();
            //委托观察者模式总结
            //1.定义一个事件数据类，有事件要用到的数据 如这里的UserEventArgs 2.定义一个订阅系统 ，初始化时传入发布者和事件参数，实现发布者的事件
            //
        }
    }
}
