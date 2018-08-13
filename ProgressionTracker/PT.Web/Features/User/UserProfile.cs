using AutoMapper;

namespace PT.Web.Features.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DAL.Entities.User, UserViewModel>();
        }
    }
}