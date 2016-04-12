using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schrage
{
    public class TasksBlock
    {
        public Task b { get; set; }
        public List<Task> block { get; set; } // a = block[0]

        public TasksBlock(List<Task> block, Task b)
        {
            this.block = block;
            this.b = b;
        }
    }
}
