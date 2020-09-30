using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using TaskManager.Infrastructure.Arguments;
using TaskManager.View;

namespace TaskManager.ViewModel
{
    public class EditMyTaskViewModel : INotifyPropertyChanged
    {
        public MyTaskViewModel NewMyTask { get; set; }
        private ListOfMyTasksViewModel viewModel;
        public EditMyTaskViewModel(string id, ListOfMyTasksViewModel viewModel)
        {
            var myTask = Resource.getInstance().MyTaskStorage.GetMyTask(id);
            if (myTask != null)
            {
                NewMyTask = new MyTaskViewModel(myTask);
            }
            this.viewModel = viewModel;
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MyTaskStorage.Edit(CreateMyTaskArgs());
                      MessageBox.Show("Success!");
                      viewModel.UpdateListOfSubjects();
                      Menu.Frame.NavigationService.GoBack();
                  }));
            }
        }

        public MyTaskArgs CreateMyTaskArgs()
        {
            return new MyTaskArgs()
            {
                Id = NewMyTask.Id.ToString(),
                Name = NewMyTask.Name,
                DeadLine = Convert.ToDateTime(NewMyTask.DeadLine),
                Type = NewMyTask.SelectedType.Content.ToString(),
                SubjectId = Resource.getInstance().SubjectStorage.GetAllSubjects().First(x => x.Name == NewMyTask.SelectedSubject.Content.ToString()).Id,
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
