using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.WorkoutsType
{
    public class DeleteWorkoutTypeCommandHandler : AsyncRequestHandler<DeleteWorkoutTypeCommand>
    {
        private readonly PtContext _db;

        public DeleteWorkoutTypeCommandHandler(PtContext db)
        {
            _db = db;
        }

        protected override async Task Handle(DeleteWorkoutTypeCommand request, CancellationToken cancellationToken)
        {
            var workoutType = await _db.WorkoutTypes.FindAsync(request.Id) ??
                              throw new ArgumentException($"No workout type found with Id: {request.Id}");

            _db.WorkoutTypes.Remove(workoutType);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}