using MediatR;

namespace PT.Web.Features.User
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }
        public long? TelegramChatId { get; set; }
        public string AspNetUsersId { get; set; }
    }
}