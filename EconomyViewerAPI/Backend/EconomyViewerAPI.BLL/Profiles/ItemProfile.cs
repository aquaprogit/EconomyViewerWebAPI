using AutoMapper;

using EconomyViewerAPI.DAL.DTO;
using EconomyViewerAPI.DAL.Entities;

namespace EconomyViewerAPI.BLL.Profiles;
public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<Item, ItemDTO>().ReverseMap();
        CreateMap<Item, ItemPutDTO>().ReverseMap();
    }
}
