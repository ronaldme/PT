using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;

namespace PT.Web.Features.User
{
    public class CreateUserCommandHandler : AsyncRequestHandler<CreateUserCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<DAL.Entities.User>(request);
            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}