using MediatR;

namespace PT.Web.Features.Workouts
{
    public class SetFinishedCommand : IRequest
    {
        public int Id { get; set; }
        public bool Finished { get; set; }
    }
}