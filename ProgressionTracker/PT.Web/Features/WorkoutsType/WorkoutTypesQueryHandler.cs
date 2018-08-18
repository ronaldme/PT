using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EFCore;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.WorkoutsType
{
    public class WorkoutTypesQueryHandler : IRequestHandler<WorkoutTypesQuery, List<WorkoutTypeViewModel>>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public WorkoutTypesQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<WorkoutTypeViewModel>> Handle(WorkoutTypesQuery request, CancellationToken cancellationToken)
        {
            return await _db.WorkoutTypes
                .ProjectToListAsync<WorkoutTypeViewModel>(_mapper.ConfigurationProvider);
        }
    }
}