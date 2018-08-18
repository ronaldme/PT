using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Web.Features.Workouts
{
    public class CreateWorkoutCommandHandler : AsyncRequestHandler<CreateWorkoutCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public CreateWorkoutCommandHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.SingleAsync(u => u.AspNetUsersId == request.UserId, cancellationToken);
            _db.Workouts.Add(new Workout
            {
                Date = request.Date.Value,
                WorkoutTypeId = request.SelectedWorkoutType,
                User = user
            });
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}