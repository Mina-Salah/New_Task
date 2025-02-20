using Microsoft.EntityFrameworkCore;
using Task_1.Models;
using Task_1.Context;

namespace Task_1.Service
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            // استرجاع آخر كود طالب
            var lastCode = await GetLastStudentCodeAsync();

            // زيادة الكود بواحد
            var newCodeNumber = int.Parse(lastCode.Split('-')[1]) + 1;
            var newCode = $"STD-{newCodeNumber:D3}";

            // تعيين الكود للطالب الجديد
            student.Code = newCode;

            // إضافة الطالب إلى قاعدة البيانات
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetLastStudentCodeAsync()
        {
            var lastStudent = await _context.Students
                .OrderByDescending(s => s.Code)
                .FirstOrDefaultAsync();

            if (lastStudent == null || string.IsNullOrEmpty(lastStudent.Code))
            {
                return "STD-000"; // إذا لم يكن هناك طلاب، نبدأ من STD-000
            }

            return lastStudent.Code;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            // التأكد من وجود الطالب في قاعدة البيانات
            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent == null)
            {
                throw new Exception("Student not found");
            }

            // تحديث البيانات الأخرى باستثناء الكود
            existingStudent.Name = student.Name;
            // يمكنك إضافة المزيد من الحقول إذا كانت موجودة

            // تحديث بيانات الطالب في قاعدة البيانات
            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

   
    }
}