using Task_1.Models;
using Task_1.Repositories;

namespace Task_1.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Course> Courses { get; }
        IGenericRepository<Student> Students { get; }
        IGenericRepository<StudentCourse> StudentCourses { get; }
        Task<int> CompleteAsync();
    }
}
