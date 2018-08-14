using MediatR;

namespace PT.Web.Features.User
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long? TelegramChatId { get; set; }
    }
}