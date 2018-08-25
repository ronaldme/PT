using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.Exercises
{
    public class AddEditExerciseQueryHandler : IRequestHandler<AddEditExerciseQuery, UpsertExerciseCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public AddEditExerciseQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UpsertExerciseCommand> Handle(AddEditExerciseQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Id.HasValue)
            {
                var exercise = await _db.Exercises.FindAsync(request.Id) ??
                               throw new ArgumentException($"No exercise found with Id: {request.Id}");
                return new UpsertExerciseCommand
                {
                    Id = request.Id,
                    Name = exercise.Name
                };
            }

            return new UpsertExerciseCommand();
        }
    }
}