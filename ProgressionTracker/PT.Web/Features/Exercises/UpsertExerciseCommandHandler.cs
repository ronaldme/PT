using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.Exercises
{
    public class UpsertExerciseCommandHandler : AsyncRequestHandler<UpsertExerciseCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public UpsertExerciseCommandHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(UpsertExerciseCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}