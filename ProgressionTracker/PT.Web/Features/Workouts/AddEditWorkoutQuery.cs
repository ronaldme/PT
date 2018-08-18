using MediatR;

namespace PT.Web.Features.Workouts
{
    public class AddEditWorkoutQuery : IRequest<AddEditWorkoutViewModel>
    {
        public int? Id { get; set; }
    }
}