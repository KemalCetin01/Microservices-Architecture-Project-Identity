using AutoMapper;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.Commands;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.Queries;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Queries;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;
using MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Handlers.User.Queries.Filters;
using MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;
using MS.Services.Identity.Application.Handlers.UserEmployees.Queries;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;
using MS.Services.Identity.Domain.Filters.UserB2CFilters;
using MS.Services.Identity.Domain.Filters.UserFilters;

namespace MS.Services.Identity.Application.Mappers;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap(typeof(ListResponse<>), typeof(PagedResponse<>));
        CreateMap(typeof(SearchListModel<>), typeof(PagedResponse<>));
        CreateMap(typeof(SearchListModel<>), typeof(SearchListModel<>));
        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
        CreateMap<Sort, SortModel>();
        CreateMap<Pagination, PaginationModel>();
        CreateMap(typeof(SearchQuery<,>), typeof(SearchQueryModel<>));

        CreateMap<GetAllGeneralCurrentAccountsQueryFilter, GetAllGeneralCurrentAccountsQueryFilterModel>();

        CreateMap(typeof(PagedResponse<>), typeof(SearchListModel<>));

        #region CurrentAccount
        CreateMap<CurrentAccount, CreateCurrentAccountCommand>().ReverseMap();
        CreateMap<CurrentAccountDTO, CurrentAccount>().ReverseMap();
        CreateMap<CurrentAccount, CurrentAccountDTO>();
        CreateMap<GeneralCurrentAccountsDTO, CurrentAccount>().ReverseMap()
            .ForMember(dest => dest.CurrentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CurrentCode, opt => opt.MapFrom(src => src.Code))
            ;

        #endregion

        CreateMap<CreateB2BUserCommand, User>().ReverseMap();
        CreateMap<CreateB2BUserCommandDTO, User>().ReverseMap();
        CreateMap<B2BSignupCommandDTO, User>().ReverseMap();
        CreateMap<B2BSignupCommandDTO, UserB2B>().ReverseMap();

        CreateMap<CreateB2BUserCommand, UserB2B>()
             .ForMember(dest => dest.UserEmployeeId, opt => opt.MapFrom(src => src.RepresentativeId));
        CreateMap<CreateB2BUserCommandDTO, UserB2B>()
             .ForMember(dest => dest.UserEmployeeId, opt => opt.MapFrom(src => src.RepresentativeId));
        CreateMap<UserB2B, CreateB2BUserCommand>()
           .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.UserEmployeeId));


        CreateMap<UpdateB2BUserCommand, User>().ReverseMap();

        CreateMap<UpdateB2BUserCommand, UserB2B>()
            .ForMember(dest => dest.UserEmployeeId, opt => opt.MapFrom(src => src.RepresentativeId));
        CreateMap<UserB2B, UpdateB2BUserCommand>()
           .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.UserEmployeeId));


        CreateMap<CreateB2CUserCommandDTO, User>().ReverseMap();


        CreateMap<UpdateB2CUserCommandDTO, User>().ReverseMap();


        CreateMap<B2BUserGetByIdDTO, UserB2B>().ReverseMap()
            .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.UserEmployeeId))
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.IdentityRefId, opt => opt.MapFrom(src => src.User.IdentityRefId));

        CreateMap<UserQueryFilter, UserB2CQueryServiceFilter>().ReverseMap();
        CreateMap<UserQueryFilter, UserB2BQueryServiceFilter>().ReverseMap();


        CreateMap<LabelValueResponse, LabelValueModel>().ReverseMap();
        CreateMap<LabelIntValueResponse, LabelIntValueModel>().ReverseMap();

      
        CreateMap<LabelValueResponse, SectorDTO>().ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        
        CreateMap<LabelValueResponse, CurrentAccountDTO>().ReverseMap()
          .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
          .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.currentAccountName));
        CreateMap<UserQueryFilter, UserQueryServiceFilter>().ReverseMap();


        #region employee
        CreateMap<SearchUserEmployeesQueryFilter, SearchUserEmployeesQueryFilterModel>();
        CreateMap<SearchEmployeeRolesQueryFilter, SearchUserEmployeeRolesQueryFilterModel>();
        CreateMap<UserEmployee, UserEmployeeDTO>()
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
             .ForMember(dest => dest.IdentityRefId, opt => opt.MapFrom(src => src.User.IdentityRefId))
            .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src => src.EmployeeRole.DiscountRate))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.EmployeeRole.Id))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeRole.Description));


      

        CreateMap<LabelValueResponse, EmployeeRoleKeyValueDTO>().ReverseMap();
        CreateMap<EmployeeRole, EmployeeRoleDTO>();
       


        #endregion

     
        CreateMap<Sector, SectorDTO>().ReverseMap();
     
        CreateMap<LabelIntValueResponse, SectorDTO>().ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        }
}