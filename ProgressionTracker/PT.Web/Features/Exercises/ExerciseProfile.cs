using AutoMapper;
using PT.DAL.Entities;

namespace PT.Web.Features.Exercises
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseViewModel>();
            CreateMap<UpsertExerciseCommand, Exercise>();
        }
    }
}