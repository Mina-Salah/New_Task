using Task_1.Models;

namespace Task_1.Service
{
    public interface IStudentCourseService
    {
        Task<IEnumerable<StudentCourse>> GetAllStudentCoursesAsync();
        Task<StudentCourse> GetStudentCourseByIdAsync(int id);
        Task AddStudentCourseAsync(StudentCourse studentCourse);
        Task UpdateStudentCourseAsync(StudentCourse studentCourse);
        Task DeleteStudentCourseAsync(int id);
    }

}
