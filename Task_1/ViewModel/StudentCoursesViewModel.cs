namespace Task_1.ViewModel
{
    public class StudentCoursesViewModel
    {
        public int StudentId { get; set; }       
        public string StudentName { get; set; }    
        public List<string> CourseNames { get; set; } = new List<string>();     }
}
