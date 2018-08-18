using AutoMapper;
using PT.DAL.Entities;

namespace PT.Web.Features.Workouts
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<Workout, WorkoutViewModel>();
            CreateMap<CreateWorkoutCommand, Workout>();
        }
    }
}