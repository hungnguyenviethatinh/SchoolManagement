using AutoMapper;
using SchoolManagement.DAL.Models;
using SchoolManagement.DAL.ViewModels;

namespace SchoolManagement.API.MapperProfiles
{
    public class TeacherMapperProfile : Profile
    {
        public TeacherMapperProfile()
        {
            CreateMap<TeacherAddModel, Teacher>()
                .ForMember(model => model.Students, map => map.Ignore());

            CreateMap<Teacher, TeacherViewModel>()
                .ForMember(viewModel => viewModel.Students, map => map.MapFrom(model => model.Students));

            CreateMap<TeacherViewModel, Teacher>()
                .ForMember(model => model.Students, map => map.Ignore());

            CreateMap<Teacher, DashboardViewModel>()
                .ForMember(
                    viewModel => viewModel.Students,
                    map => map.MapFrom(model => model.Students.OrderBy(s => s.DateOfBirth)));
        }
    }
}
