using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lockfree4net
{
    /// <summary>
    /// Lock-free thread safe stack
    /// Unlike  System.Collections.Concurrent.ConcurrentStackT> this queue supported at .net 2.0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentStack<T>
    {
        private volatile StackItem<T> _head;

        private volatile bool _isEmpty = true;
        public bool IsEmpty { get { return _isEmpty; } }

        public T Pop()
        {
            StackItem<T> prevHead;
            do
            {
                prevHead = _head;

            } while (Interlocked.CompareExchange(ref _head, _head.Next, prevHead) != prevHead);
            return prevHead.Value;
        }

        public void Push(T item)
        {
            var newItem = new StackItem<T>(item, _head);
            StackItem<T> prevHead;
            do
            {
                prevHead = _head;
                newItem.Next = prevHead;

            } while (Interlocked.CompareExchange(ref _head, newItem, prevHead) != prevHead);
        }
    }

    class StackItem<T>
    {
        public T Value;
        public volatile StackItem<T> Next;

        public StackItem(T value, StackItem<T> next)
        {
            Value = value;
            Next = next;
        }
    }
}
