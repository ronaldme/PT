using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EFCore;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.Workouts
{
    public class WorkoutsQueryHandler : IRequestHandler<WorkoutsQuery, List<WorkoutViewModel>>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public WorkoutsQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<WorkoutViewModel>> Handle(WorkoutsQuery request, CancellationToken cancellationToken)
        {
            return await _db.Workouts
                .Where(w => (request.UpcomingWorkouts ? w.Date >= DateTime.Now.Date : w.Date < DateTime.Now) &&
                            w.User.AspNetUsersId == request.UserId)
                .OrderBy(w => w.Date)
                .ProjectToListAsync<WorkoutViewModel>(_mapper.ConfigurationProvider);
        }
    }
}