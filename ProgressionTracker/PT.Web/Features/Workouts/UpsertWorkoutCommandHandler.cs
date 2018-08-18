using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Web.Features.Workouts
{
    public class UpsertWorkoutCommandHandler : AsyncRequestHandler<UpsertWorkoutCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public UpsertWorkoutCommandHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(UpsertWorkoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.SingleAsync(u => u.AspNetUsersId == request.UserId, cancellationToken);

            if (request.Id.HasValue)
            {
                var workout = await _db.Workouts.FindAsync(request.Id) ??
                    throw new ArgumentException($"No workout found with Id: {request.Id}");

                workout.Date = request.Date.Value;
                workout.WorkoutTypeId = request.SelectedWorkoutType;
            }
            else
            {
                _db.Workouts.Add(new Workout
                {
                    Date = request.Date.Value,
                    WorkoutTypeId = request.SelectedWorkoutType,
                    User = user
                });
            }

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}