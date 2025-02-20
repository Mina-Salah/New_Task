using Task_1.Models;
using Task_1.UnitOfWork;

namespace Task_1.Service
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _unitOfWork.Courses.GetAllAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _unitOfWork.Courses.GetByIdAsync(id);
        }

        public async Task AddCourseAsync(Course course)
        {
            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _unitOfWork.Courses.Update(course);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.CompleteAsync();
        }
    }

}
