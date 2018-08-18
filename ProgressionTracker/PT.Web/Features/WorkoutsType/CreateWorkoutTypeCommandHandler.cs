using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Web.Features.WorkoutsType
{
    public class CreateWorkoutTypeCommandHandler : AsyncRequestHandler<CreateWorkoutTypeCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public CreateWorkoutTypeCommandHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(CreateWorkoutTypeCommand request, CancellationToken cancellationToken)
        {
            var workout = _mapper.Map<WorkoutType>(request);
            _db.WorkoutTypes.Add(workout);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}