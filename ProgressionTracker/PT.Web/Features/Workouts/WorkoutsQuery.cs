using System.Collections.Generic;
using MediatR;

namespace PT.Web.Features.Workouts
{
    public class WorkoutsQuery : IRequest<List<WorkoutViewModel>>
    {
        public bool UpcomingWorkouts { get; set; }
        public string UserId { get; set; }
    }
}