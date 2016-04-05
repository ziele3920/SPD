using System;
using System.Collections;
//using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schrage
{
    public struct Task
    {
        public int r; //czs dostępności
        public int t; //czas trwania
        public int q; //czas dostarczenia

        public Task(Task task)
        {
            r = task.r;
            t = task.t;
            q = task.q;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TaskService TS = new TaskService();

            while (true)
            {

                Console.WriteLine("podaj nazwe pliku");
                string filename = Console.ReadLine();
                UnreadyTaskQueue unready = new UnreadyTaskQueue(TS.ReadData(filename));
                Console.WriteLine("sorted data");
                unready.Display();

                int time = unready.GetFirst().r;
                int finishTime = 0;
                ReadyTaskQueue ready = new ReadyTaskQueue();

                while(unready.getLength() > 0 || ready.getLength() >0)
                {
                    ready.Add(unready.GetTasksReadyAt(time));
                    if (ready.getLength() > 0)
                    {
                        Task task = ready.eraseFirst();
                        finishTime = Math.Max(finishTime, task.t + time + task.q);
                        time = task.t + time; continue;
                    }
                }

                Console.WriteLine("\nfinsh at");
                Console.WriteLine(finishTime);
            }
        }
    }
}
