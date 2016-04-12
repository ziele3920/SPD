using System.Collections.Generic;

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

        public List<TasksBlock> Split(List<Task> tasks)
        {
            int finishTime = 0;
            Task b = null;
            List<TasksBlock> blocksList = new List<TasksBlock>();
            List<Task> block = new List<Task>();
            Task lastTask = tasks[0];
            block.Add(lastTask);

            for(int i = 1; i < tasks.Count; ++i)
            {
                if (tasks[i].startTime == lastTask.startTime + lastTask.t)
                {
                    lastTask = tasks[i];
                    block.Add(lastTask);
                    if(finishTime < lastTask.startTime+lastTask.t + lastTask.q)
                    {
                        finishTime = lastTask.startTime + lastTask.t + lastTask.q;
                        b = lastTask;
                    }

                }
                else
                {
                    blocksList.Add(new TasksBlock(block, b));
                    block = new List<Task>();
                    lastTask = tasks[i];
                    block.Add(lastTask);
                    finishTime = 0;
                }

            }
            return blocksList;
        }

    }
}
