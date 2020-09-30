using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManager.Model;
using WorldOfWords.Infrastructure.Arguments;

namespace TaskManager.Infrastructure.Storage
{
    public class SubjectStorage : ISubjectStorage
    {
        private readonly TaskManagerDbContext _context;

        public SubjectStorage(TaskManagerDbContext context)
        {
            _context = context;
        }
        public void Create(SubjectArgs args)
        {
            var subject = new Subject()
            {
                Id = Guid.NewGuid(),
                Name = args.Name,
                DateLection = args.DateLection,
                DatePractice = args.DatePractice,
                Lector = args.Lector,
                Tasks = args.Tasks,
                Teacher = args.Teacher,
            };

            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }
        // AsEnumerable must to add if using SQLite, without it Linq is`nt work.
        public Subject GetSubject(string id)
        {
            return _context.Subjects.AsEnumerable().FirstOrDefault(x => x.Id.ToString() == id);
        }
        // AsEnumerable must to add if using SQLite, without it Linq is`nt work.
        public List<Subject> GetAllSubjects()
        {
            return _context.Subjects.AsEnumerable().ToList();
        }
        public void Edit(SubjectArgs args)
        {
            var subject = GetSubject(args.Id);
            subject.Name = args.Name;
            subject.Teacher = args.Teacher;
            subject.Lector = args.Lector;
            subject.DateLection = args.DateLection;
            subject.DatePractice = args.DatePractice;
            _context.SaveChanges();
        }
        public void Delete(string id)
        {
            var subject = GetSubject(id);
            _context.Subjects.Remove(subject);
            _context.SaveChanges();
        }
    }
}
