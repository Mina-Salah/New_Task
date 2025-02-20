using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Task_1.Models;
using Task_1.UnitOfWork;

namespace Task_1.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentCourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var studentCourses = await _unitOfWork.StudentCourses.GetAllAsync();
            return View(studentCourses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Students = new SelectList(await _unitOfWork.Students.GetAllAsync(), "Id", "Name");
            ViewBag.Courses = new SelectList(await _unitOfWork.Courses.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.StudentCourses.AddAsync(studentCourse);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = new SelectList(await _unitOfWork.Students.GetAllAsync(), "Id", "Name", studentCourse.StudentId);
            ViewBag.Courses = new SelectList(await _unitOfWork.Courses.GetAllAsync(), "Id", "Name", studentCourse.CourseId);
            return View(studentCourse);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var studentCourse = await _unitOfWork.StudentCourses.GetByIdAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            ViewBag.Students = new SelectList(await _unitOfWork.Students.GetAllAsync(), "Id", "Name", studentCourse.StudentId);
            ViewBag.Courses = new SelectList(await _unitOfWork.Courses.GetAllAsync(), "Id", "Name", studentCourse.CourseId);
            return View(studentCourse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentCourse studentCourse)
        {
            if (id != studentCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.StudentCourses.Update(studentCourse);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = new SelectList(await _unitOfWork.Students.GetAllAsync(), "Id", "Name", studentCourse.StudentId);
            ViewBag.Courses = new SelectList(await _unitOfWork.Courses.GetAllAsync(), "Id", "Name", studentCourse.CourseId);
            return View(studentCourse);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var studentCourse = await _unitOfWork.StudentCourses.GetByIdAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }
            return View(studentCourse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentCourse = await _unitOfWork.StudentCourses.GetByIdAsync(id);
            _unitOfWork.StudentCourses.Delete(studentCourse);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
