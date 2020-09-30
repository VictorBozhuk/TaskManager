using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using TaskManager.Infrastructure.Arguments;

namespace TaskManager.ViewModel
{
    public class CreateMyTaskViewModel : INotifyPropertyChanged
    {
        public MyTaskViewModel NewMyTask { get; set; }
        public CreateMyTaskViewModel()
        {
            NewMyTask = new MyTaskViewModel();
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          Resource.getInstance().MyTaskStorage.Create(CreateMyTaskArgs());
                      }
                      catch(Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                          return;
                      }
                      ResetNewWord();
                      MessageBox.Show("Success!");
                  }));
            }
        }

        public void ResetNewWord()
        {
            NewMyTask.Name = null;
            NewMyTask.DeadLine = null;
            NewMyTask.SelectedSubject = NewMyTask.Subjects.First();
        }

        public MyTaskArgs CreateMyTaskArgs()
        {
            var subjects = Resource.getInstance().SubjectStorage.GetAllSubjects();
            if(string.IsNullOrEmpty(NewMyTask.Name))
            {
                throw new Exception("The field of Name can't be empty!");
            }
            if(string.IsNullOrEmpty(NewMyTask.DeadLine))
            {
                throw new Exception("The field of DeadLine can't be empty!");
            }
            try
            {
                return new MyTaskArgs()
                {
                    Name = NewMyTask.Name,
                    Type = NewMyTask.SelectedType.Content.ToString(),
                    DeadLine = Convert.ToDateTime(NewMyTask.DeadLine),
                    Subject = subjects.First(x => x.Name == NewMyTask.SelectedSubject.Content.ToString()),
                    SubjectId = subjects.First(x => x.Name == NewMyTask.SelectedSubject.Content.ToString()).Id,
                };
            }
            catch(FormatException)
            {
                throw new Exception("The field of DeadLine wrong inputed!");
            }
            catch(InvalidOperationException)
            {
                throw new Exception("Select subject. This field can't be empty.");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
