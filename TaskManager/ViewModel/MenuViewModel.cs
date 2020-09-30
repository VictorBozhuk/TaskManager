using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.View;

namespace TaskManager.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public MenuViewModel()
        {
        }

        private RelayCommand tasksCommand;
        public RelayCommand TasksCommand
        {
            get
            {
                return tasksCommand ??
                  (tasksCommand = new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new ListOfMyTasks());
                  }));
            }
        }

        private RelayCommand subjectsCommand;
        public RelayCommand SubjectsCommand
        {
            get
            {
                return subjectsCommand ??
                  (subjectsCommand = new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new ListOfSubjects());
                  }));
            }
        }

        private RelayCommand createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return createCommand ??
                  (createCommand = new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new CreateMyTask());
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
