using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WorldOfWords.Infrastructure.Arguments;

namespace TaskManager.ViewModel
{
    public class CreateSubjectViewModel : INotifyPropertyChanged
    {
        public SubjectViewModel NewSubject { get; set; }
        private ListOfSubjectsViewModel viewModel;
        public CreateSubjectViewModel(ListOfSubjectsViewModel viewModel)
        {
            NewSubject = new SubjectViewModel();
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
                      try
                      {
                        Resource.getInstance().SubjectStorage.Create(CreateSubjectArgs());
                      }
                      catch(Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                          return;
                      }
                      ResetNewSubject();
                      MessageBox.Show("Success!");
                      viewModel.UpdateListOfSubjects();
                  }));
            }
        }

        public void ResetNewSubject()
        {
            NewSubject.Name = null;
            NewSubject.Lector = null;
            NewSubject.Teacher = null;
            NewSubject.DateLection = null;
            NewSubject.DatePractice = null;
        }

        public SubjectArgs CreateSubjectArgs()
        {
            if(string.IsNullOrEmpty(NewSubject.Name))
            {
                throw new Exception("Please, enter name.");
            }
            return new SubjectArgs()
            {
                Name = NewSubject.Name,
                Lector = NewSubject.Lector,
                Teacher = NewSubject.Teacher,
                DateLection = NewSubject.DateLection,
                DatePractice = NewSubject.DatePractice,
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
