using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Model;
using WorldOfWords.Infrastructure.Arguments;

namespace TaskManager.Infrastructure.Storage
{
    public interface ISubjectStorage
    {
        void Create(SubjectArgs args);
        Subject GetSubject(string id);
        List<Subject> GetAllSubjects();
        void Edit(SubjectArgs args);
        void Delete(string id);
    }
}
