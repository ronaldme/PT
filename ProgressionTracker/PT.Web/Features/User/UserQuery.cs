using System;
using MediatR;

namespace PT.Web.Features.User
{
    public class UserQuery : IRequest<UserViewModel>
    {
        public string AspNetUsersId { get; set; }
    }
}