using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task_1.Context;
using Task_1.Models;
using Task_1.UnitOfWork;
using Task_1.ViewModel;

namespace Task_1.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public StudentCourseController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var studentCourses = await _context.StudentCourses
                .Include(sc => sc.Student) // تأكد من تحميل بيانات الطالب
                .Include(sc => sc.Course)  // تأكد من تحميل بيانات الكورس
                .ToListAsync();

            var groupedData = studentCourses
                .Where(sc => sc.Student != null && sc.Course != null)
                .GroupBy(sc => sc.Student.Id)
                .Select(group => new StudentCoursesListViewModel
                {
                    StudentId = group.Key,
                    StudentName = group.First().Student.Name,
                    CourseNames = group.Select(sc => sc.Course.Name).ToList()
                })
                .ToList();

            return View(groupedData);
        }


        public async Task<IActionResult> Create()
        {
            var viewModel = new StudentCoursesViewModel
            {
                Students = new SelectList(await _unitOfWork.Students.GetAllAsync(), "Id", "Name"),
                Courses = new SelectList(await _unitOfWork.Courses.GetAllAsync(), "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCoursesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // لكل كورس مختار، نقوم بإنشاء سجل منفصل في جدول StudentCourse
                foreach (var courseId in viewModel.CourseIds)
                {
                    var studentCourse = new StudentCourse
                    {
                        StudentId = viewModel.StudentId,
                        CourseId = courseId
                    };
                    await _unitOfWork.StudentCourses.AddAsync(studentCourse);
                }
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            // في حال فشل التحقق، إعادة تعبئة قوائم الاختيار
            viewModel.Students = new SelectList(await _unitOfWork.Students.GetAllAsync(), "Id", "Name", viewModel.StudentId);
            viewModel.Courses = new SelectList(await _unitOfWork.Courses.GetAllAsync(), "Id", "Name");
            return View(viewModel);
        }

        // تبقى باقي الأكشن كما هي (Edit, Delete, ...)، ويمكنك تعديلها لاحقًا إذا أردت
        public async Task<IActionResult> Edit(int id)
        {
            // هنا تحتاج إلى معالجة خاصة إذا أردت تعديل مجموعة الكورسات الخاصة بطلاب،
            // مثلاً قراءة كافة الكورسات المرتبطة وإظهارها مع التشيك بوكس مع التحديد المناسب.
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
