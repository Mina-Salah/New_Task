using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task_1.ViewModel
{
    public class StudentCoursesViewModel
    {
        public int StudentId { get; set; }
        public List<int> CourseIds { get; set; } = new List<int>();

        // القوائم المستخدمة في عناصر الاختيار (DropDown/CheckBox)
        public IEnumerable<SelectListItem>? Students { get; set; }
        public IEnumerable<SelectListItem>? Courses { get; set; }
    }
}
