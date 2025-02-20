using Microsoft.EntityFrameworkCore;
using Task_1.Models;
using Task_1.UnitOfWork;

namespace Task_1.Service
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentCourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentCourse>> GetAllStudentCoursesAsync()
        {
            return await _unitOfWork.StudentCourses
                .QueryWithInclude(sc => sc.Student, sc => sc.Course)
                .ToListAsync();
        }
        public async Task<StudentCourse> GetStudentCourseByIdAsync(int id)
        {
            return await _unitOfWork.StudentCourses.GetByIdAsync(id);
        }

        public async Task AddStudentCourseAsync(StudentCourse studentCourse)
        {
            await _unitOfWork.StudentCourses.AddAsync(studentCourse);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateStudentCourseAsync(StudentCourse studentCourse)
        {
            _unitOfWork.StudentCourses.Update(studentCourse);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteStudentCourseAsync(int id)
        {
            var studentCourse = await _unitOfWork.StudentCourses.GetByIdAsync(id);
            _unitOfWork.StudentCourses.Delete(studentCourse);
            await _unitOfWork.CompleteAsync();
        }
    }

}
