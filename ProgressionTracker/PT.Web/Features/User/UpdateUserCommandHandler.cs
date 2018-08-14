using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PT.DAL;

namespace PT.Web.Features.User
{
    public class UpdateUserCommandHandler : AsyncRequestHandler<UpdateUserCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FindAsync(request.Id);
            _mapper.Map(request, user);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}