using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Infrastructure.Arguments;
using TaskManager.Model;

namespace TaskManager.Infrastructure.Storage
{
    public interface IMyTaskStorage
    {
        void Create(MyTaskArgs args);
        MyTask GetMyTask(string id);
        List<MyTask> GetAllMyTasks();
        void Edit(MyTaskArgs args);
        void Delete(string id);
    }
}
