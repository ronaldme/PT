using MediatR;

namespace PT.Web.Features.Workouts
{
    public class DeleteWorkoutCommand : IRequest
    {
        public int Id { get; set; }
    }
}