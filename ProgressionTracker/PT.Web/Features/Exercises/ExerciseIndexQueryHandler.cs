using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;
using PT.Web.Infrastructure;
using X.PagedList;

namespace PT.Web.Features.Exercises
{
    public class ExerciseIndexQueryHandler : IRequestHandler<ExerciseIndexQuery, IPagedList<ExerciseViewModel>>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public ExerciseIndexQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IPagedList<ExerciseViewModel>> Handle(ExerciseIndexQuery request, CancellationToken cancellationToken)
        {
            // todo: add handling for pagesize / pagenumber
            request.PageSize = 10;
            request.PageNumber = 1;
            return await _db.Exercises.ProjectToPagedListAsync<ExerciseViewModel>(_mapper.ConfigurationProvider,
                request.PageNumber, request.PageSize);
        }
    }
}