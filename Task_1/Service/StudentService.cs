using Task_1.Models;
using Task_1.UnitOfWork;

namespace Task_1.Service
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.Students.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _unitOfWork.Students.Update(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            _unitOfWork.Students.Delete(student);
            await _unitOfWork.CompleteAsync();
        }
    }

}
