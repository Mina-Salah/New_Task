﻿namespace Task_1.ViewModel
{
    public class StudentCoursesListViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<string> CourseNames { get; set; } = new List<string>();
    }
}
