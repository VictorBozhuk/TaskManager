using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Model;

namespace TaskManager.Infrastructure.Arguments
{
    public class MyTaskArgs
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DeadLine { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Type { get; set; }
    }
}
