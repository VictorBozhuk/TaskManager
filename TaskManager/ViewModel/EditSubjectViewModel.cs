using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TaskManager.View;
using WorldOfWords.Infrastructure.Arguments;

namespace TaskManager.ViewModel
{
    public class EditSubjectViewModel : INotifyPropertyChanged
    {
        public SubjectViewModel NewSubject { get; set; }
        private ListOfSubjectsViewModel listOfSubjectViewModel;
        public EditSubjectViewModel(string id, ListOfSubjectsViewModel viewModel)
        {
            var subject = Resource.getInstance().SubjectStorage.GetSubject(id);
            if (subject != null)
            {
                NewSubject = new SubjectViewModel()
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    DateLection = subject.DateLection,
                    DatePractice = subject.DatePractice,
                    Lector = subject.Lector,
                    Teacher = subject.Teacher,
                };
                listOfSubjectViewModel = viewModel;
            }
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
                          Resource.getInstance().SubjectStorage.Edit(CreateSubjectArgs());
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                          return;
                      }
                      ResetNewSubject();
                      MessageBox.Show("Success!");
                      listOfSubjectViewModel.UpdateListOfSubjects();
                      ListOfSubjects.CreateEdit.Navigate(new CreateSubject(listOfSubjectViewModel));
                  }));
            }
        }

        private RelayCommand undoCommand;
        public RelayCommand UndoCommand
        {
            get
            {
                return undoCommand ??
                  (undoCommand = new RelayCommand(obj =>
                  {
                      ListOfSubjects.CreateEdit.Navigate(new CreateSubject(listOfSubjectViewModel));
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
            if (string.IsNullOrEmpty(NewSubject.Name))
            {
                throw new Exception("Please, enter name.");
            }
            return new SubjectArgs()
            {
                Id = NewSubject.Id.ToString(),
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
