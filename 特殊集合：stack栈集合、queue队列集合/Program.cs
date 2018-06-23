using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace 特殊集合_stack栈集合_queue队列集合
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.stack栈集合；又名 干草堆集合 栈集合 //特点：（1）一个一个赋值 一个一个取值
            //（2）先进后出 
            Stack st = new Stack();
            //添加元素用push
            st.Push(1);
            st.Push(2);
            st.Push(3);
            st.Push(4);
            st.Push(5);
            //输出个数
            Console.WriteLine("个数：" + st.Count);

            //只要使用一次pop方法，就会从最后一个元素开始排除 弹出
            Console.WriteLine("要移除最顶部对象：" + st.Pop());
            Console.ReadKey();
            Console.WriteLine("再次移除最顶部对象：" + st.Pop());

            // 只想查看不弹出
            Console.WriteLine("查看顶部对象：" + st.Peek());
            //遍历集合
            foreach (int aa in st)
            {
                Console.WriteLine("循环的剩余对象：" + aa);
            }
            //2，queue队列集合；特点：先进先出
            Queue que = new Queue();
            //添加元素
            que.Enqueue(1);
            que.Enqueue(2);
            que.Enqueue(3);
            que.Enqueue(4);
            que.Enqueue(5);
            //移除一个元素 从头开始
           
            Console.WriteLine("移除的对象：" +  que.Dequeue());
            Console.WriteLine("移除后剩余对象数："+que.Count);

            //遍历集合
            foreach (int aa in que)
            {
                Console.WriteLine(aa);
            }
            //3泛型的Stack<T>  push入 pop出
            Stack<string> st2 = new Stack<string>();
            Console.WriteLine("请输入一串字符到Stack<T>");
            string s = Console.ReadLine();
            char[] a = s.ToCharArray();
            foreach (var item in a)
            {
                st2.Push(item.ToString());
                Console.WriteLine("正在入栈："+item.ToString() );
            }
            while (st2.Count > 0)
            {
                Console.WriteLine("正在移除："+st2.Pop());
            }
            //4Queue<T>泛型 Enqueue入  Dequeue出
            Console.WriteLine();
            Queue<int> st4 = new Queue<int>();
            int[] a4 = { 0, 1,2, 3, 4, 5 };
            foreach (var item in a4)
            {
                st4.Enqueue(item);
                Console.WriteLine("正在入队：" + item.ToString());
            }
            while (st4.Count > 0)
            {
               
                Console.WriteLine("正在出队：" + st4.Dequeue());
            }
            Console.ReadKey();
        }
    }
}
