using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using EconomyViewerAPI.DAL.DTO;
using EconomyViewerAPI.DAL.Entities;

namespace EconomyViewerAPI.BLL.Profiles;
public class ServerProfile : Profile
{
    private readonly IMapper _mapper;
    public ServerProfile()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Item, ItemDTO>().ReverseMap()));
        CreateMap<Server, ServerDTO>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(s => s.Items.Select(i => _mapper.Map<ItemDTO>(i))));
    }
}
