using System;
using System.Data.Entity;
using TaskManager.Model;

namespace TaskManager.Infrastructure
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<MyTask> MyTasks { get; set; }

        public TaskManagerDbContext() : base("DbConnection")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Word>().Property(x => x.LastUpdate).IsOptional();
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
