using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;

namespace WorldOfWords.Infrastructure.Arguments
{
    public class SubjectArgs
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Lector { get; set; }
        public string Teacher { get; set; }
        public string DateLection { get; set; }
        public string DatePractice { get; set; }
        public ICollection<MyTask> Tasks { get; set; }

    }
}
