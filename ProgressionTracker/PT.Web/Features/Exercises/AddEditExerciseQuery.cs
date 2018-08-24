using MediatR;

namespace PT.Web.Features.Exercises
{
    public class AddEditExerciseQuery : IRequest<UpsertExerciseCommand>
    {
        public int? Id { get; set; }
    }
}