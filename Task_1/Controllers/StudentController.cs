using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task_1.Models;
using Task_1.Service;

namespace Task_1.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> Create()
        {
            // استرجاع آخر كود طالب
            var lastCode = await _studentService.GetLastStudentCodeAsync();

            // زيادة الكود بواحد
            var newCodeNumber = int.Parse(lastCode.Split('-')[1]) + 1;
            var newCode = $"STD-{newCodeNumber:D3}";

            // إنشاء نموذج جديد وتعيين الكود
            var student = new Student
            {
                Code = newCode
            };

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}