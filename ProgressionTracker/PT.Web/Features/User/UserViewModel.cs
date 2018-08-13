namespace PT.Web.Features.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string AspNetUsersId { get; set; }
        public virtual long? TelegramChatId { get; set; }
        
    }
}