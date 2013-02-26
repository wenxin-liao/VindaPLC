using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PLCDeviceMonitor
{
    public interface ThreadPoolWorker
    {
        void DoWork(Object p);
    }

    public class MonitorThreadPool
    {
        public ShareQueue<MonitorTask> InputQueue
        {
            get;
            set;
        }
        public ShareQueue<MonitorTask> OutputQueue
        {
            get;
            set;
        }
        public ShareQueue<MonitorTask> PendingQueue
        {
            get;
            set;
        }

        private bool m_Started = false;
        private List<Thread> m_WorkThreads = new List<Thread>();

        public void Start(int count, ThreadPoolWorker worker, String name)
        {
            if (!m_Started)
            {
                for (int i = 0; i < count; ++i)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(worker.DoWork)) { IsBackground = true, Name = name };
                    thread.Start(this);

                    m_WorkThreads.Add(thread);
                }
                m_Started = true;
            }
        }

        public void Join()
        {
            foreach (Thread t in m_WorkThreads)
                t.Join();

            m_WorkThreads.Clear();
        }
    }
}
