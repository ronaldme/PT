using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Web.Features.Workouts
{
    public class CreateWorkoutHandler : AsyncRequestHandler<CreateWorkoutCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public CreateWorkoutHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
        {
            var workout = _mapper.Map<Workout>(request);
            _db.Workouts.Add(workout);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}