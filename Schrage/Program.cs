using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schrage
{
    public class Task
    {
        public int r; //czs dostępności
        public int t; //czas trwania
        public int q; //czas dostarczenia
        public int startTime; //czas rozpoczecia wykonywania zdania

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
                List<Task> schrager = new List<Task>();



                UnreadyTaskQueue unready = new UnreadyTaskQueue(TS.ReadData(filename));
                //Console.WriteLine("sorted data");
                //unready.Display();
                //SchragePodz(unready);
                Carier(unready, int.MaxValue);

            }
        }

        static int Carier(UnreadyTaskQueue unready, int UB)
        {
            TaskService TS = new TaskService();
            List<Task> schrager = new List<Task>();
            int LB, q, rp, qp, pp;
            int U = Schrage(unready, schrager);
            unready = new UnreadyTaskQueue(schrager);
            if (U < UB)
                UB = U;
            TasksBlock lastBlock = TS.Split(schrager);
            if (lastBlock.c == null)
                return UB;
            rp = TS.FindMinR(lastBlock);
            pp = TS.FindSumP(lastBlock);
            qp = TS.FindMinQ(lastBlock);

            lastBlock.c.r = Math.Max(lastBlock.c.r, rp + pp); //sprwadzic jak nie dziala
            int copyR = lastBlock.c.r;
            LB = SchragePodz(unready);
            unready = new UnreadyTaskQueue(schrager);
            if (LB < UB)
                Carier(unready, UB);
            lastBlock.c.r = copyR;

            lastBlock.c.q = Math.Max(lastBlock.c.q, qp + pp);
            int copyQ = lastBlock.c.q;
            LB = SchragePodz(unready);
            unready = new UnreadyTaskQueue(schrager);
            if (LB < UB)
                Carier(unready, UB);
            lastBlock.c.q = copyQ;

            return -1;


        }

        private static int Schrage(UnreadyTaskQueue unready, List<Task> schrager)
        {
            int time = unready.GetROfFirstTask();
            int finishTime = 0;
            ReadyTaskQueue ready = new ReadyTaskQueue();

            while (!unready.IsEmpty() || !ready.IsEmpty())
            {
                ready.Add(unready.GetTasksReadyAt(time));
                if (!ready.IsEmpty())
                {
                    Task task = ready.eraseFirst();
                    task.startTime = time;
                    schrager.Add(task);
                    finishTime = Math.Max(finishTime, task.t + time + task.q);
                    time = task.t + time; continue;
                }
                if (ready.IsEmpty())
                    time = unready.GetROfFirstTask();
            }

            Console.WriteLine("\nfinsh at");
            Console.WriteLine(finishTime);
            return finishTime;
        }

        private static int SchragePodz(UnreadyTaskQueue unready)
        {

            int time = unready.GetROfFirstTask();
            int finishTime = 0;
            ReadyTaskQueue ready = new ReadyTaskQueue();
            Task task = null;

            while (!unready.IsEmpty() || !ready.IsEmpty())
            {
                ready.Add(unready.GetTasksReadyAt(ref time, task, ready));

                if (!ready.IsEmpty())
                {
                    task = ready.eraseFirst();
                    task.startTime = time;
                    //schrager.Add(task);
                    finishTime = Math.Max(finishTime, task.t + time + task.q);
                    time = task.t + time; continue;
                }

                if (ready.IsEmpty())
                    time = unready.GetROfFirstTask();

            }

            Console.WriteLine("\nfinsh at");
            Console.WriteLine(finishTime);
            return finishTime;
            
        }
    }
}
