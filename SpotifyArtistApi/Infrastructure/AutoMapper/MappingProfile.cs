using AutoMapper;
using SpotifyArtistApi.Domain;
using SpotifyArtistApi.Models.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyArtistApi.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ArtistModel>()
                .ForMember(x => x.Artist_id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Artist_name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
