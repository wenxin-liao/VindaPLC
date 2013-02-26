using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace PLCDeviceMonitor
{
    /// <summary>
    /// Thread safe queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ShareQueue<T>
    {
        public ShareQueue()
        {
            IsShutdown = false;
            m_Queue = new Queue<T>();
        }

        public bool IsShutdown
        {
            get;
            private set;
        }

        public void Shutdown()
        {
            IsShutdown = true;

            lock (m_Queue)
            {
                Monitor.PulseAll(m_Queue);
            }
        }

        public void Enqueue(T item)
        {
            if (IsShutdown)
                throw new Exception("Queue is shutdown.");

            lock(m_Queue)
            {
                m_Queue.Enqueue(item);
                Monitor.Pulse(m_Queue);
            }
        }

        public T Dequeue()
        {
            if (IsShutdown)
                throw new Exception("Queue is shutdown.");

            T item = default(T);

            lock (m_Queue)
            {
                if (m_Queue.Count == 0)
                    Monitor.Wait(m_Queue);

                if (!IsShutdown && m_Queue.Count > 0)
                    item = m_Queue.Dequeue();
                else
                    throw new Exception("Queue is shutdown.");
            }

            return item;
        }

        private Queue<T> m_Queue;
    }
}
