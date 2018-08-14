using MediatR;

namespace PT.Web.Features.Workouts
{
    public class CreateWorkoutCommand : IRequest
    {
        public string Name { get; set; }
    }
}