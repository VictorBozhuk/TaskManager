using System;
using System.Collections;
using System.Collections.Generic;

namespace TaskManager.Model
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lector { get; set; }
        public string Teacher { get; set; }
        public string DateLection { get; set; }
        public string DatePractice { get; set; }
        public ICollection<MyTask> Tasks { get; set; }
    }
}
