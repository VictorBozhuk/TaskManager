using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    public class SubjectViewModel : INotifyPropertyChanged
    {
        private string name;
        private string lector;
        private string teacher;
        private string dateLection;
        private string datePractice;

        public SubjectViewModel()
        {
        }
        public SubjectViewModel(Subject subject, int index = 1)
        {
            Id = subject.Id;
            Name = subject.Name;
            Lector = subject.Lector;
            Teacher = subject.Teacher;
            DateLection = subject.DateLection;
            DatePractice = subject.DatePractice;

            if (index % 2 == 0)
            {
                RowColor = new SolidColorBrush(Colors.Black);
            }
            else
            {
                RowColor = null;
            }
        }

        public Brush RowColor { get; set; }

        public Guid Id { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Lector
        {
            get { return lector; }
            set
            {
                lector = value;
                OnPropertyChanged("Lector");
            }
        }
        public string Teacher
        {
            get { return teacher; }
            set
            {
                teacher = value;
                OnPropertyChanged("Teacher");
            }
        }
        public string DateLection
        {
            get { return dateLection; }
            set
            {
                dateLection = value;
                OnPropertyChanged("DateLection");
            }
        }
        public string DatePractice
        {
            get { return datePractice; }
            set
            {
                datePractice = value;
                OnPropertyChanged("DatePractice");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
