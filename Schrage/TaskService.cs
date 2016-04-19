﻿using System.Collections.Generic;

namespace Schrage
{
    class TaskService
    {

        public PriorityQueue<int, Task> ReadData(string filename)
        {
            PriorityQueue<int, Task> data = new PriorityQueue<int, Task>();
            string directory = System.IO.Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(directory + "\\" + filename);


            for (int i = 1; i < lines.Length; ++i)
            {
                Task currentTask = new Task();
                string[] line = lines[i].Split(' ');
                currentTask.r = int.Parse(line[0]);
                currentTask.t = int.Parse(line[1]);
                currentTask.q = int.Parse(line[2]);
                data.Enqueue(currentTask.r, currentTask);
            }
            return data;
        }

        public TasksBlock Split(List<Task> tasks)
        {
            
            Task lastTask = tasks[tasks.Count-1];
            int finishTime = lastTask.startTime + lastTask.t + lastTask.q;
            TasksBlock block = new TasksBlock();
            block.block.Add(lastTask);
            block.b = lastTask;
            int qb = lastTask.q;
            bool cWasFound = false;

            for (int i = tasks.Count-2; i > -1; --i)
            {
                if (tasks[i].startTime + tasks[i].t == lastTask.startTime)
                {
                    lastTask = tasks[i];
                    block.block.Insert(0, lastTask);
                    if(finishTime < lastTask.startTime+lastTask.t + lastTask.q)
                    {
                        finishTime = lastTask.startTime + lastTask.t + lastTask.q;
                        block.b = lastTask;
                    }
                    if (!cWasFound && lastTask.q < qb)
                    {
                        block.c = lastTask;
                        cWasFound = true;
                    }

                }
                else
                {
                    block.a = lastTask;
                    return block;
                }

            }
            return null;
        }

    }
}
