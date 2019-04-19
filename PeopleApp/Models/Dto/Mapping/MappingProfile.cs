using AutoMapper;
using Sphaera.Web.Api.Extensions;
using Sphaera.Web.Core.Cards;
//using Sphaera.Web.Server.Models;
//using Sphaera.Web.Server.Models.Cards;

namespace Sphaera.Web.PeoplePublicArea.Models.Dto.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDto>().IgnoreAllNonExisting().ReverseMap()
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
                .IgnoreAllNonExisting();
            CreateMap<CardsList, CardsListDto>();

            CreateMap<CardStatstic, CardStatsticDto>().IgnoreAllNonExisting();

            CreateMap<CardFilterDto, CardFilter>().IgnoreAllNonExisting();
        }
    }
}