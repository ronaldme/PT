using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PT.DAL;

namespace PT.Web.Features.Workouts
{
    public class CreateWorkoutQueryHandler : IRequestHandler<CreateWorkoutQuery, CreateWorkoutViewModel>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public CreateWorkoutQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CreateWorkoutViewModel> Handle(CreateWorkoutQuery request, CancellationToken cancellationToken)
        {
            return new CreateWorkoutViewModel
            {
                WorkoutTypes = await _db.WorkoutTypes
                    .ToDictionaryAsync(w => w.Id, w => w.Name, cancellationToken),
                Date = DateTime.Now
            };
        }
    }
}