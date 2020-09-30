using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Storage;

namespace TaskManager
{
    public class Resource
    {
        private static Resource instance;

        public Resource()
        {
            SubjectStorage = new SubjectStorage(new TaskManagerDbContext());
            MyTaskStorage = new MyTaskStorage(new TaskManagerDbContext());
        }

        public static Resource getInstance()
        {
            if (instance == null)
                instance = new Resource();
            return instance;
        }

        public ISubjectStorage SubjectStorage { get; set; }
        public IMyTaskStorage MyTaskStorage { get; set; }
    }
}
