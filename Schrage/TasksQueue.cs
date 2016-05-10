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
                //Console.WriteLine("task r "+ ts.r + " dla t " + time);
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

        public Queue<Task> GetTasksReadyAt(ref int time, Task currentTask, ReadyTaskQueue currentReady)
        {
            Queue<Task> ready = new Queue<Task>();
            while (!list.IsEmpty)
            {
                //Console.WriteLine("WSZEDŁ");
                Task ts = list.Dequeue();
                //Console.WriteLine("task r "+ ts.r + " dla t " + time);
                if (ts.r <= time)
                {
                    ready.Enqueue(ts);
                    if(currentTask != null && ts.q > currentTask.q)
                    {
                        currentTask.t = time - ts.r;
                        time = ts.r;
                        if (currentTask != null && currentTask.t > 0)
                           currentReady.Add(currentTask);
                    }
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

        public UnreadyTaskQueue(List<Task> tasks)
        {
            list = new PriorityQueue<int, Task>();
            foreach (Task t in tasks)
                list.Enqueue(t.r, t);
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

        public void Add(Task task)
        {
            list.Enqueue(-task.q, task);
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

