using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.Workouts
{
    public class SetFinishedCommandCommandHandler : AsyncRequestHandler<SetFinishedCommand>
    {
        private readonly PtContext _db;

        public SetFinishedCommandCommandHandler(PtContext db)
        {
            _db = db;
        }

        protected override async Task Handle(SetFinishedCommand request, CancellationToken cancellationToken)
        {
            var workout = await _db.Workouts.FindAsync(request.Id) ??
                          throw new ArgumentException($"No workout found with Id: {request.Id}");

            workout.Finished = request.Finished;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}