using AutoMapper;
using PT.DAL.Entities;

namespace PT.Web.Features.WorkoutsType
{
    public class WorkoutTypeProfile : Profile
    {
        public WorkoutTypeProfile()
        {
            CreateMap<CreateWorkoutTypeCommand, WorkoutType>();
            CreateMap<WorkoutType, WorkoutTypeViewModel>();
            
        }
    }
}