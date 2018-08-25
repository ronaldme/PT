using MediatR;

namespace PT.Web.Features.Exercises
{
    public class UpsertExerciseCommand : IRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}