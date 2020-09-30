using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using TaskManager.View;

namespace TaskManager.ViewModel
{
    public class ListOfMyTasksViewModel : INotifyPropertyChanged
    {
        private int daysInterval = 100;
        private string type = "Everything";
        public MyTaskViewModel SelectedMyTask { get; set; }
        public ObservableCollection<MyTaskViewModel> MyTasks { get; set; }

        public ListOfMyTasksViewModel()
        {
            MyTasks = new ObservableCollection<MyTaskViewModel>(GetMyTasks());
        }

        private List<MyTaskViewModel> GetMyTasks()
        {
            var myTasks = Resource.getInstance().MyTaskStorage.GetAllMyTasks();
            return myTasks.Select(x => new MyTaskViewModel(x, myTasks.IndexOf(x))).ToList();
        }

        internal void UpdateListOfSubjects()
        {
            MyTasks.Clear();
            List<MyTaskViewModel> newMyTasks = GetMyTasks();

            foreach (var item in newMyTasks)
            {
                MyTasks.Add(item);
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MyTaskStorage.Delete(SelectedMyTask.Id.ToString());
                      MyTasks.RemoveAt(MyTasks.IndexOf(SelectedMyTask));
                  }));
            }
        }


        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {
                      Menu.Frame.Navigate(new EditMyTask(SelectedMyTask.Id.ToString(), this));
                  }));
            }
        }

        private RelayCommand timeCommand;
        public RelayCommand TimeCommand
        {
            get
            {
                return timeCommand ??
                  (timeCommand = new RelayCommand(obj =>
                  {
                      daysInterval = Convert.ToInt32(obj.ToString());
                  }));
            }
        }

        private RelayCommand filterCommand;
        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ??
                  (filterCommand = new RelayCommand(obj =>
                  {
                      FilrerListOfMyTasks();
                  }));
            }
        }

        private RelayCommand typeCommand;
        public RelayCommand TypeCommand
        {
            get
            {
                return typeCommand ??
                  (typeCommand = new RelayCommand(obj =>
                  {
                      type = obj.ToString();
                  }));
            }
        }

        private void FilrerListOfMyTasks()
        {
            MyTasks.Clear();
            List<MyTaskViewModel> newMyTasks = new List<MyTaskViewModel>();
            if (type == "Everything")
            {
                newMyTasks = GetMyTasks()
                    .Where(x => DateTime.Now.AddDays(daysInterval) >= Convert.ToDateTime(x.DeadLine) 
                    && DateTime.Now <= Convert.ToDateTime(x.DeadLine)).ToList();
            }
            else
            {
                newMyTasks = GetMyTasks()
                    .Where(x => DateTime.Now.AddDays(daysInterval) >= Convert.ToDateTime(x.DeadLine)
                    && DateTime.Now <= Convert.ToDateTime(x.DeadLine)).Where(x => x.SelectedType.Content.ToString() == type).ToList();
            }

            foreach (var item in newMyTasks)
            {
                MyTasks.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
