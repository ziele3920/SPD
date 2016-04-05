using System.Collections.Generic;

namespace Schrage
{
    class TaskService
    {
        public List<Task> ReadData(string filename)
        {
            List<Task> data = new List<Task>();
            string directory = System.IO.Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(directory + "\\" + filename);

            for (int i = 1; i < lines.Length; ++i)
            {
                Task currentTask = new Task();
                string[] line = lines[i].Split(' ');
                currentTask.r = int.Parse(line[0]);
                currentTask.t = int.Parse(line[1]);
                currentTask.q = int.Parse(line[2]);
                data.Add(currentTask);
            }
            return data;
        }

        

    }
}
