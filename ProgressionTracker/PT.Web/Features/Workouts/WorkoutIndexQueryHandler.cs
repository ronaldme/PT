using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;
using PT.Web.Infrastructure;

namespace PT.Web.Features.Workouts
{
    public class WorkoutIndexQueryHandler : IRequestHandler<WorkoutIndexQuery, WorkoutIndexViewModel>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public WorkoutIndexQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<WorkoutIndexViewModel> Handle(WorkoutIndexQuery request, CancellationToken cancellationToken)
        {
            var workouts = await _db.Workouts
                .Where(w => w.Date >= DateTime.Now.Date)
                .OrderBy(w => w.Date)
                .Where(u => u.User.AspNetUsersId == request.UserId)
                .ProjectToPagedListAsync<WorkoutViewModel>(_mapper.ConfigurationProvider, request.PageNumber, request.PageSize);

            return new WorkoutIndexViewModel
            {
                PagedList = workouts,
            };
        }
    }
}