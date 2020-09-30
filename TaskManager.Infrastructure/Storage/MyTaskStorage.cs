using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using TaskManager.Infrastructure.Arguments;
using TaskManager.Model;

namespace TaskManager.Infrastructure.Storage
{
    public class MyTaskStorage : IMyTaskStorage
    {
        private readonly TaskManagerDbContext _context;

        public MyTaskStorage(TaskManagerDbContext context)
        {
            _context = context;
        }

        public void Create(MyTaskArgs args)
        {
            var myTask = new MyTask()
            {
                Id = Guid.NewGuid(),
                Name = args.Name,
                DeadLine = args.DeadLine,
                SubjectId = args.SubjectId,
                Type = args.Type,
            };

            _context.MyTasks.Add(myTask);
            _context.SaveChanges();
        }
        // AsEnumerable must to add if using SQLite, without it Linq is`nt work.
        public MyTask GetMyTask(string id)
        {
            return _context.MyTasks.AsEnumerable().First(x => x.Id.ToString() == id);
        }
        // AsEnumerable must to add if using SQLite, without it Linq is`nt work.
        public List<MyTask> GetAllMyTasks()
        {
            return _context.MyTasks.Include("Subject").AsEnumerable().OrderBy(x => x.DeadLine).ToList();
        }
        public void Edit(MyTaskArgs args)
        {
            var myTask = GetMyTask(args.Id);
            myTask.Name = args.Name;
            myTask.Type = args.Type;
            myTask.DeadLine = args.DeadLine;
            myTask.SubjectId = args.SubjectId;

            _context.SaveChanges();
        }
        public void Delete(string id)
        {
            var myTask = GetMyTask(id);
            _context.MyTasks.Remove(myTask);
            _context.SaveChanges();
        }
    }
}
