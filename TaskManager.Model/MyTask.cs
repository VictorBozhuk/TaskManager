using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Model
{
    public class MyTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DeadLine { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Type { get; set; }
    }
}
