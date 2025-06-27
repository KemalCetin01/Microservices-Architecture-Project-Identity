using AutoMapper;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.ActivityAreas.DTOs;
using MS.Services.Identity.Application.Handlers.ActivityAreas.Queries;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Handlers.B2bRoles.Queries;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Application.Handlers.CurrentAccountNotes.Queries;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.Commands;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.Queries;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.DTOs;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Queries;
using MS.Services.Identity.Application.Handlers.Notes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Application.Handlers.Notes.Queries;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.DTOs;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;
using MS.Services.Identity.Application.Handlers.Occupations.DTOs;
using MS.Services.Identity.Application.Handlers.Occupations.Queries;
using MS.Services.Identity.Application.Handlers.Positions.DTOs;
using MS.Services.Identity.Application.Handlers.Positions.Queries;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;
using MS.Services.Identity.Application.Handlers.Sectors.Queries;
using MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;
using MS.Services.Identity.Application.Handlers.User.Commands.B2CUser;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Handlers.User.Queries.B2C;
using MS.Services.Identity.Application.Handlers.User.Queries.Filters;
using MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;
using MS.Services.Identity.Application.Handlers.UserEmployees.Queries;
using MS.Services.Identity.Application.Handlers.UserNotes.Queries;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Filters.AddressFilters;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;
using MS.Services.Identity.Domain.Filters.UserB2CFilters;
using MS.Services.Identity.Domain.Filters.UserFilters;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Application.Handlers.BusinessNotes.Queries;
using MS.Services.Identity.Application.DTOs.AddressLocation.Response;
using MS.Services.Identity.Application.Handlers.Business.Queries;

namespace MS.Services.Identity.Application.Mappers;

public class BusinessMappingProfile : Profile
{
    public BusinessMappingProfile()
    {

        CreateMap<GetAllGeneralCurrentAccountsQueryFilter, GetAllGeneralCurrentAccountsQueryFilterModel>();
        
        CreateMap<SearchBusinessRolesQueryFilter, SearchBusinessRolesQueryFilterModel>();

        CreateMap<Business, GetBusinessResponseDto>()
            .ForMember(dest => dest.BusinessStatus, opt => opt.MapFrom(src => src.BusinessStatusEnum));
        CreateMap<Business, UpdateBusinessReviewStatusResponseDto>();
        CreateMap<Business, SearchBusinessResponseDto>()
            .ForMember(dest => dest.CountryCode, 
                opt => opt.MapFrom(src => src.BillingAddress != null ? src.BillingAddress.AddressLocation.CountryCode : null))
            .ForMember(dest => dest.BusinessStatus, 
                opt => opt.MapFrom(src => src.BusinessStatusEnum))
            .ForMember(dest => dest.Note, 
                opt => opt.MapFrom(src => src.Notes == null ? null : src.Notes.Count == 0 ? null : src.Notes.Where(x => x.Note != null && x.Note.IsDeleted == false).OrderByDescending(x => x.Note.CreatedDate).FirstOrDefault().Note.Content));
        
        CreateMap<UserEmployee, GetBusinessRepresentativeResponseDto>()
            .ForMember(dest => dest.Id,
                opts => opts
                    .MapFrom(src => src.UserId))
            .ForMember(dest => dest.FirstName,
                opts => opts
                    .MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName,
                opts => opts
                    .MapFrom(src => src.User.LastName));
                    
        CreateMap<Business, LabelValueResponse>()
            .ForMember(dest => dest.Value,
                opts => opts
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.Label,
                opts => opts
                    .MapFrom(src => src.Name));

        CreateMap<BusinessBillingAddress, GetBusinessBillingAddressResponseDto>();
        CreateMap<AddressLocation, GetAddressLocationResponseDto>();

        CreateMap<SearchBusinessesQueryFilter, BusinessSearchFilter>();

        CreateMap<SearchBusinessUsersQueryFilter, BusinessUserSearchFilter>();
        CreateMap<UserB2B, SearchBusinessUsersResponseDto>()
            .ForMember(dest => dest.FullName, 
                opts => opts.MapFrom(src => String.Concat(src.User.FirstName, " ", src.User.LastName)))
            .ForMember(dest => dest.Email, 
                opts => opts.MapFrom(src => src.User.Email));

        CreateMap<SearchBusinessCurrentAcccountsQueryFilter, BusinessCurrentAccountSearchFilter>();
        CreateMap<CurrentAccount, SearchBusinessCurrentAccountsResponseDto>()
            .ForMember(dest => dest.Id, 
                opts => opts.MapFrom(src => src.Id));
    }
}