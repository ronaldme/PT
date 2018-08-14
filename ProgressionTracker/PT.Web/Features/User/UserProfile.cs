using AutoMapper;

namespace PT.Web.Features.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DAL.Entities.User, UserViewModel>();
            CreateMap<CreateUserCommand, DAL.Entities.User>();
            CreateMap<UpdateUserCommand, DAL.Entities.User>();
        }
    }
}