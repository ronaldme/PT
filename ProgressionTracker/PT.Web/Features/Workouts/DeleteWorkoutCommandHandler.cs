using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.Workouts
{
    public class DeleteWorkoutCommandHandler : AsyncRequestHandler<DeleteWorkoutCommand>
    {
        private readonly PtContext _db;

        public DeleteWorkoutCommandHandler(PtContext db)
        {
            _db = db;
        }

        protected override async Task Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
        {
            var workout = await _db.Workouts.FindAsync(request.Id) ??
                          throw new ArgumentException($"No workout found with Id: {request.Id}");

            _db.Workouts.Remove(workout);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}