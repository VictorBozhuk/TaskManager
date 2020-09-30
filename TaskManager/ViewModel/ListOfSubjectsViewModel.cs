using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using TaskManager.View;

namespace TaskManager.ViewModel
{
    public class ListOfSubjectsViewModel : INotifyPropertyChanged
    {
        public SubjectViewModel SelectedSubject { get; set; }
        public ObservableCollection<SubjectViewModel> Subjects { get; set; }

        public ListOfSubjectsViewModel()
        {
            Subjects = new ObservableCollection<SubjectViewModel>(GetSubjects());
        }

        private List<SubjectViewModel> GetSubjects()
        {
            var subjects = Resource.getInstance().SubjectStorage.GetAllSubjects();
            return subjects.Select(x => new SubjectViewModel(x, subjects.IndexOf(x))).ToList();
        }

        internal void UpdateListOfSubjects()
        {
            Subjects.Clear();
            List<SubjectViewModel> newSubjects = GetSubjects();

            foreach (var item in newSubjects)
            {
                Subjects.Add(item);
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
                      Resource.getInstance().SubjectStorage.Delete(SelectedSubject.Id.ToString());
                      Subjects.RemoveAt(Subjects.IndexOf(SelectedSubject));
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
                      ListOfSubjects.CreateEdit.Navigate(new EditSubject(SelectedSubject.Id.ToString(), this));

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
