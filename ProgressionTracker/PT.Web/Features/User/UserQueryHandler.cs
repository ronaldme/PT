using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PT.DAL;

namespace PT.Web.Features.User
{
    public class UserQueryHandler : IRequestHandler<UserQuery, UserViewModel>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public UserQueryHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.AspNetUsersId == request.AspNetUsersId, cancellationToken);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}