using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lockfree4net.PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(0);
            stack.Push(1);
            stack.Push(2);
            var res = stack.Pop();
            res = stack.Pop();
            stack.Pop();
            Console.ReadLine();
        }
    }
}
