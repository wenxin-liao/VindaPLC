using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace PLCDeviceMonitorGUI
{
    /// <summary>
    /// Thread safe queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ShareQueue<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ShareQueue()
        {
            IsShutdown = false;
            m_Queue = new Queue<T>();
        }

        /// <summary>
        /// Property that indicate whether the queue is shutdown
        /// </summary>
        public bool IsShutdown
        {
            get;
            private set;
        }

        /// <summary>
        /// Shutdown the queue
        /// </summary>
        public void Shutdown()
        {
            IsShutdown = true;

            lock (m_Queue)
            {
                Monitor.PulseAll(m_Queue);
            }
        }

        /// <summary>
        /// Add an item to the end of the queue
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <exception>
        /// Throw exception when the queue is shutdown
        /// </exception>
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

        /// <summary>
        /// Remove and return the item that at the beginning of the queue
        /// </summary>
        /// <returns>Item that is removed</returns>
        /// <exception>
        /// Throw exception when the queue is shutdown
        /// </exception>
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

        /// <summary>
        /// Private members
        /// </summary>
        private Queue<T> m_Queue;
    }
}
