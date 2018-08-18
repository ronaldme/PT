using MediatR;

namespace PT.Web.Features.Workouts
{
    public class WorkoutIndexQuery : IRequest<WorkoutIndexViewModel>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}