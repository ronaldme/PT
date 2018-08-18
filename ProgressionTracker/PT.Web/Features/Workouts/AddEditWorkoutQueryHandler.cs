using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PT.DAL;

namespace PT.Web.Features.Workouts
{
    public class AddEditWorkoutQueryHandler : IRequestHandler<AddEditWorkoutQuery, AddEditWorkoutViewModel>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public AddEditWorkoutQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<AddEditWorkoutViewModel> Handle(AddEditWorkoutQuery request, CancellationToken cancellationToken)
        {
            var workoutTypes = await _db.WorkoutTypes
                .ToDictionaryAsync(w => w.Id, w => w.Name, cancellationToken);

            if (request.Id.HasValue)
            {
                var workout = await _db.Workouts.FindAsync(request.Id) ??
                              throw new ArgumentException($"No workout found with Id: {request.Id}");

                return new AddEditWorkoutViewModel
                {
                    Id = request.Id,
                    WorkoutTypes = workoutTypes,
                    Date = workout.Date,
                    SelectedWorkoutType = workout.WorkoutTypeId
                };
            }

            return new AddEditWorkoutViewModel
            {
                WorkoutTypes = workoutTypes,
                Date = DateTime.Now
            };
        }
    }
}