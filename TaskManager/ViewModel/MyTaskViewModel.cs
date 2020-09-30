using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    public class MyTaskViewModel : INotifyPropertyChanged
    {
        private string name;
        private string deadLine;
        private string subject;
        private string type;
        private string teacher;
        private ComboBoxItem selectedSubject;
        private ComboBoxItem selectedType;

        public ObservableCollection<ComboBoxItem> Subjects { get; set; }
        public ObservableCollection<ComboBoxItem> Types { get; set; }

        public MyTaskViewModel()
        {
            Types = GetTypes();
            SelectedType = Types.First();

            Subjects = new ObservableCollection<ComboBoxItem>();
            Subjects.Add(new ComboBoxItem() { Content = "Select subject" });
            SelectedSubject = Subjects.First();
            var subjects = Resource.getInstance().SubjectStorage.GetAllSubjects();
            foreach (var item in subjects)
            {
                Subjects.Add(new ComboBoxItem() { Content = item.Name });
            }
        }

        public MyTaskViewModel(MyTask task, int index = 1)
        {
            Subjects = new ObservableCollection<ComboBoxItem>();
            var subjects = Resource.getInstance().SubjectStorage.GetAllSubjects();
            foreach (var item in subjects)
            {
                if(task.Subject.Name == item.Name)
                {
                    Subjects.Add(new ComboBoxItem() { Content = item.Name });
                    SelectedSubject = Subjects.Last();
                }
                else
                {
                    Subjects.Add(new ComboBoxItem() { Content = item.Name });
                }
            }

            Types = GetTypes();
            SelectedType = Types.FirstOrDefault(x => x.Content.ToString() == task.Type);

            Id = task.Id;
            Name = task.Name;
            DeadLine = task.DeadLine.ToString();
            Subject = task.Subject.Name;
            Type = task.Type;
            Teacher = task.Subject.Teacher;

            if (index % 2 == 0)
            {
                RowColor = new SolidColorBrush(Colors.Black);
            }
            else
            {
                RowColor = null;
            }
        }

        private ObservableCollection<ComboBoxItem> GetTypes()
        {
            return new ObservableCollection<ComboBoxItem>()
            {
                new ComboBoxItem() {Content = "Home work"},
                new ComboBoxItem() {Content = "Test"},
            };
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
        public string DeadLine
        {
            get { return deadLine; }
            set
            {
                deadLine = value;
                OnPropertyChanged("DeadLine");

            }
        }

        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
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

        public ComboBoxItem SelectedSubject
        {
            get { return selectedSubject; }
            set
            {
                selectedSubject = value;
                OnPropertyChanged("SelectedSubject");
            }
        }

        public ComboBoxItem SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
