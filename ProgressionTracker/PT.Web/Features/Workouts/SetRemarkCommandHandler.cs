using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.Workouts
{
    public class SetRemarkCommandHandler : AsyncRequestHandler<SetRemarkCommand>
    {
        private readonly PtContext _db;

        public SetRemarkCommandHandler(PtContext db)
        {
            _db = db;
        }

        protected override async Task Handle(SetRemarkCommand request, CancellationToken cancellationToken)
        {
            var workout = await _db.Workouts.FindAsync(request.Id) ?? 
                          throw new ArgumentException($"No workout found with Id: {request.Id}");

            workout.Remark = request.Remark;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}