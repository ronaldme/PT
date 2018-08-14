using AutoMapper;
using PT.DAL.Entities;

namespace PT.Web.Features.PlannedWorkouts
{
    public class PlannedWorkoutProfile : Profile
    {
        public PlannedWorkoutProfile()
        {
            CreateMap<PlannedWorkout, PlannedWorkoutViewModel>();
        }
    }
}