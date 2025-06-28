using AutoMapper;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.Queries;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Mappers;

public class BusinessMappingProfile : Profile
{
    public BusinessMappingProfile()
    {

        CreateMap<GetAllGeneralCurrentAccountsQueryFilter, GetAllGeneralCurrentAccountsQueryFilterModel>();
        
                    
        CreateMap<Business, LabelValueResponse>()
            .ForMember(dest => dest.Value,
                opts => opts
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.Label,
                opts => opts
                    .MapFrom(src => src.Name));

    }
}