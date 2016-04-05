using System;
using System.Collections.Generic;

namespace Schrage
{
    public class UnreadyTaskQueue
    {
        PriorityQueue<int, Task> list;

        public bool IsEmpty()
        {
            return list.IsEmpty;
        }

        public Task eraseFirst()
        {
            return list.Dequeue();
        }

        public int GetROfFirstTask()
        {
            Task t = list.Dequeue();
            int time = t.r;
            list.Enqueue(t.r, t);
            return time;
        }
        public Queue<Task> GetTasksReadyAt(int time)
        { 
            Queue<Task> ready = new Queue<Task>();
            while(!list.IsEmpty)
            {
                //Console.WriteLine("WSZEDŁ");
                Task ts = list.Dequeue();
                Console.WriteLine("task r "+ ts.r + " dla t " + time);
                if (ts.r <= time)
                {
                    ready.Enqueue(ts);
                }
                else
                {
                    list.Enqueue(ts.r, ts);
                    break;
                }
            }
            return ready;
        }

        public UnreadyTaskQueue(PriorityQueue<int, Task> newList)
        {
            list = newList;
        }

        public void Display()
        {
            PriorityQueue<int, Task> diplayed = new PriorityQueue<int, Task>();
            while (!list.IsEmpty)
            {
                Task t = list.Dequeue();
                Console.WriteLine(t.r + " " + t.t + " " + t.q);
                diplayed.Enqueue(t.r, t);
            }
            list = diplayed;
        }
    }

    public class ReadyTaskQueue
    {
        PriorityQueue<int, Task> list;

        public Task eraseFirst()
        {
            return list.Dequeue();
        }

        public void Add(Queue<Task> tQueue)
        {
            while (tQueue.Count > 0)
            {
                Task t = tQueue.Dequeue();
                list.Enqueue(-t.q, t);
            }
        }

        public void Display()
        {
            PriorityQueue<int, Task> diplayed = new PriorityQueue<int, Task>();
            while (!list.IsEmpty)
            {
                Task t = list.Dequeue();
                Console.WriteLine(t.r + " " + t.t + " " + t.q);
                diplayed.Enqueue(t.r, t);
            }
            list = diplayed;
        }
        public bool IsEmpty()
        {
            return list.IsEmpty;
        }

        public ReadyTaskQueue()
        {
            list = new PriorityQueue<int, Task>();
        }
    }
}

