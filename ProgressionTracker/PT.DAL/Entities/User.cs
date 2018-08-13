using System.Collections.Generic;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class User : Entity
    {
        public virtual string Name { get; set; }
        public virtual long? TelegramChatId { get; set; }
        public virtual string AspNetUsersId { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }
    }
}