using AutoMapper;
using SchoolManagement.DAL.Models;
using SchoolManagement.DAL.ViewModels;

namespace SchoolManagement.API.MapperProfiles
{
    public class StudentMapperProfile : Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<StudentAddModel, Student>()
                .ForMember(model => model.Teacher, map => map.Ignore());

            CreateMap<Student, StudentViewModel>()
                .ForMember(viewModel => viewModel.TeacherName, map => map.MapFrom(model => model.Teacher.Name));

            CreateMap<StudentViewModel, Student>()
                .ForMember(model => model.Teacher, map => map.Ignore());
        }
    }
}
