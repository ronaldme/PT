using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.Exercises
{
    public class DeleteExerciseCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteExerciseCommandHandler : AsyncRequestHandler<DeleteExerciseCommand>
    {
        private readonly PtContext _db;

        public DeleteExerciseCommandHandler(PtContext db)
        {
            _db = db;
        }

        protected override async Task Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = await _db.Exercises.FindAsync(request.Id) ??
                           throw new ArgumentException($"No exercise found with Id: {request.Id}");

            _db.Exercises.Remove(exercise);

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}