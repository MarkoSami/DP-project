using AutoMapper;
using Giftify.Models;
using Giftify.Models.ViewModels;

namespace Giftify.MappingProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<RegisterVM, AppUser>();
        }
    }
}
