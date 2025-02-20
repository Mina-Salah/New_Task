using Task_1.Context;
using Task_1.Models;
using Task_1.Repositories;

namespace Task_1.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Course> Courses { get; }
        public IGenericRepository<Student> Students { get; }
        public IGenericRepository<StudentCourse> StudentCourses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Courses = new GenericRepository<Course>(_context);
            Students = new GenericRepository<Student>(_context);
            StudentCourses = new GenericRepository<StudentCourse>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
