using AutoMapper;
using Contracts;

namespace searchservice.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AuctionCreated, Item>()
.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<AuctionUpdated, Item>();
            CreateMap<AuctionDeleted, Item>();

        }
    }
}