using System;
using System.Collections.Generic;

namespace Schrage
{
    public class UnreadyTaskQueue
    {
        List<Task> list;

        public int getLength()
        {
            return list.Count;
        }

        public Task eraseFirst()
        {
            Task t = new Task(list[0]);
            list.RemoveAt(0);
            return t;
        }

        public Task GetFirst()
        {
            return list[0];
        }

        public List<Task> GetTasksReadyAt(int time)
        {
                    

            List<Task> ready = new List<Task>();
            for(int i = 0; i < list.Count; ++i)
            {
                //Console.WriteLine("WSZEDŁ");
                Task ts = list[i];
                Console.WriteLine("task r "+ ts.r + " dla t " + time);
                if (ts.r <= time)
                {
                    Task task = new Task(ts);
                    ready.Add(task);
                    if(list.Count == 1)
                    {
                        list.RemoveAt(0);
                        break;
                    }
                }
                else
                {
                    // Console.WriteLine(i);
                    //int removeRange;
                    //if (i == 0)
                    //    removeRange = 1;
                    //else if (i == list.Count)
                    //    removeRange = list.Count;
                    //else
                    //    removeRange = i;
                    //Console.WriteLine("przed");
                    //Display();
                    //Console.WriteLine("po");
                    list.RemoveRange(0, i);
                    //Display();
                    //Console.WriteLine("usuwam od 0 do " + (i) + "dla t = " + time + "na liscie zostaje " + list.Count);
                    break;
                }
            }
            return ready;
        }

        public void Add(Task newTask)
        {
            list.Add(newTask);
            list.Sort((t1, t2) => t1.r.CompareTo(t2.r));
        }

        public UnreadyTaskQueue()
        {
            list = new List<Task>();
        }

        public UnreadyTaskQueue(List<Task> newList)
        {
            list = newList;
            list.Sort((t1, t2) => t1.r.CompareTo(t2.r));
        }

        public void Display()
        {
            foreach (Task t in list)
                Console.WriteLine(t.r + " " + t.t + " " + t.q);
        }

    }

    public class ReadyTaskQueue
    {
        List<Task> list;

        public int getLength()
        {
            return list.Count;
        }

        public Task eraseFirst()
        {
            Display();
            Task t = list[0];
            list.RemoveAt(0);
            return t;
        }

        public Task GetFirst()
        {
            return list[0];
        }

        public void Add(Task newTask)
        {
            list.Add(newTask);
            list.Sort((t1, t2) => -t1.q.CompareTo(t2.q));
        }

        public void Add(List<Task> tList)
        {
            list.AddRange(tList);
            list.Sort((t1, t2) => -t1.q.CompareTo(t2.q));
        }

        public ReadyTaskQueue()
        {
            list = new List<Task>();
        }

        public ReadyTaskQueue(List<Task> newList)
        {
            list = newList;
            list.Sort((t1, t2) => -t1.q.CompareTo(t2.q));
        }

        public void Display()
        {
            foreach (Task t in list)
                Console.WriteLine(t.r + " " + t.t + " " + t.q);
        }

    }
}

