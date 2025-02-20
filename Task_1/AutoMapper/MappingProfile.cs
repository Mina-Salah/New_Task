using static System.Runtime.InteropServices.JavaScript.JSType;
using Task_1.Models;
using AutoMapper;
using Task_1.ViewModel;

namespace Task_1.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // تحويل من الكيان Student إلى StudentCoursesViewModel والعكس
            CreateMap<Student, StudentCoursesViewModel>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Course, StudentCoursesViewModel>()
                .ForMember(dest => dest.CourseNames, opt => opt.MapFrom(src => new List<string> { src.Name }));
        }
    }
}
