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

        CreateMap<SearchActivityAreasQueryFilter, SearchActivityAreasQueryFilterModel>();
        CreateMap<SearchNumberOfEmployeesQueryFilter, SearchNumberOfEmployeeQueryFilterModel>();
        CreateMap<SearchPositionsQueryFilter, SearchPositionsQueryFilterModel>();
        CreateMap<SearchOccupationsQueryFilter, SearchOccupationsQueryFilterModel>();

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

        #region User


        //CreateMap<UserDTO, User>().ReverseMap()
        //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
        //   // .ForMember(dest => dest.Representative, opt => opt.MapFrom(src => src.UserDetail.UserEmployee != null ? src.UserDetail.UserEmployee.FirstName + " " + src.UserDetail.UserEmployee.LastName : null)) //TODO
        //    .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.UserDetail.Country != null ? src.UserDetail.Country.Name : null))
        //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.UserDetail.City != null ? src.UserDetail.City.Name : null))
        //    .ForMember(dest => dest.Town, opt => opt.MapFrom(src => src.UserDetail.Town != null ? src.UserDetail.Town.Name : null))
        //    .ForMember(dest => dest.CurrentName, opt => opt.MapFrom(src => src.BusinessUsers == null ? null : src.BusinessUsers.Count < 1 ? null : src.BusinessUsers.Last().Business.CurrentAccountBusinesses.Count < 1 ? null : src.BusinessUsers.Last().Business.CurrentAccountBusinesses.Last().CurrentAccount.CompanyName))
        //    .ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.BusinessUsers == null ? null : src.BusinessUsers.Count < 1 ? null : src.BusinessUsers.Last().Business.CurrentAccountBusinesses.Count<1 ? null : src.BusinessUsers.Last().Business.CurrentAccountBusinesses.FirstOrDefault().CurrentAccount.Status.ToString()))
        //    .ForMember(dest => dest.CurrentId, opt => opt.MapFrom(src => src.BusinessUsers == null ? null : src.BusinessUsers.Count < 1 ? null : src.BusinessUsers.Last().Business.CurrentAccountBusinesses.Count<1?null: src.BusinessUsers.Last().Business.CurrentAccountBusinesses.Last().CurrentAccount.Code.ToString()))
        //    .ForMember(dest => dest.BillingCount, opt => opt.MapFrom(src => src.UserAddresses == null ? 0 : src.UserAddresses.Where(x => x.AddressTypeId == 1).ToList().Count))
        //    .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.UserNotes == null ? null : src.UserNotes.Count < 1 ? null : src.UserNotes.Where(x => x.Note.IsDeleted == false).OrderByDescending(x => x.Note.CreatedDate).FirstOrDefault().Note.Content))
        //    .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(src => src.UserDetail.UserStatus))
        //    .ForMember(dest => dest.SiteStatus, opt => opt.MapFrom(src => src.UserDetail.SiteStatus))
        //    .ForMember(dest => dest.UserProfile, opt => opt.MapFrom(src => src.UserDetail.UserProfile))
        //    .ForMember(dest => dest.BusinessId, opt => opt.MapFrom(src => src.BusinessUsers.Last().BusinessId));

/*
        CreateMap<UserDTO, UserB2C>().ReverseMap()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
           .ForMember(dest => dest.Representative, opt => opt.MapFrom(src => src.UserEmployee.User.FirstName + " " + src.UserEmployee.User.LastName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
           .ForMember(dest => dest.BusinessId, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault()!.BusinessId))
           .ForMember(dest => dest.BusinessCode, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault()!.Business.Code))
           .ForMember(dest => dest.BusinessReviewStatus, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault()!.Business.ReviewStatus))
           .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault().Business.Name))
           .ForMember(dest => dest.BusinessStatus, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault().Business.BusinessStatus))
           ;
*/
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

        CreateMap<CreateB2CUserCommandDTO, UserB2C>()
             .ForMember(dest => dest.UserEmployeeId, opt => opt.MapFrom(src => src.RepresentativeId));
        CreateMap<UserB2C, CreateB2CUserCommandDTO>()
           .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.UserEmployeeId));


        CreateMap<UpdateB2CUserCommandDTO, User>().ReverseMap();

        CreateMap<UpdateB2CUserCommandDTO, UserB2C>()
            .ForMember(dest => dest.UserEmployeeId, opt => opt.MapFrom(src => src.RepresentativeId));
        CreateMap<UserB2C, UpdateB2CUserCommandDTO>()
           .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.UserEmployeeId));


        CreateMap<B2BUserGetByIdDTO, UserB2B>().ReverseMap()
            .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.UserEmployeeId))
            .ForMember(dest => dest.BusinessId, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault().BusinessId))
            .ForMember(dest => dest.BusinessCode, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault()!.Business.Code))
            .ForMember(dest => dest.BusinessReviewStatus, opt => opt.MapFrom(src => src.BusinessUsers.FirstOrDefault()!.Business.ReviewStatus))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.IdentityRefId, opt => opt.MapFrom(src => src.User.IdentityRefId));


        CreateMap<B2CUserGetByIdDTO, UserB2C>().ReverseMap()
            .ForMember(dest => dest.RepresentativeId, opt => opt.MapFrom(src => src.UserEmployeeId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.User.CreatedDate))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id));

        CreateMap<UserQueryFilter, UserB2CQueryServiceFilter>().ReverseMap();
        CreateMap<UserQueryFilter, UserB2BQueryServiceFilter>().ReverseMap();

        #endregion

        CreateMap<LabelValueResponse, LabelValueModel>().ReverseMap();
        CreateMap<LabelIntValueResponse, LabelIntValueModel>().ReverseMap();

        CreateMap<LabelValueResponse, ActivityAreaDTO>().ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));
        CreateMap<LabelValueResponse, SectorDTO>().ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        CreateMap<LabelValueResponse, ActivityAreaDTO>().ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));
        
        CreateMap<LabelValueResponse, CurrentAccountDTO>().ReverseMap()
          .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
          .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.currentAccountName));
        CreateMap<UserQueryFilter, UserQueryServiceFilter>().ReverseMap();

        CreateMap<Note, NoteDTO>().ReverseMap();
        CreateMap<CreateNoteCommand, NoteDTO>().ReverseMap();
        CreateMap<UpdateNoteCommand, NoteDTO>().ReverseMap();
        CreateMap<UpdateNoteCommand, NoteDTO>().ReverseMap();

        CreateMap<NoteQueryFilter, NoteQueryServiceFilter>().ReverseMap();

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


        CreateMap<BusinessNoteQueryFilter, NoteQueryServiceFilter>().ReverseMap();
        CreateMap<BusinessNote, NoteDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Note.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Note.Content))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Note.Status))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Note.CreatedByUser.User.FirstName + " " + src.Note.CreatedByUser.User.LastName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Note.CreatedDate));

        CreateMap<CurrentAccountNoteQueryFilter, NoteQueryServiceFilter>().ReverseMap();
        CreateMap<CurrentAccountNote, NoteDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Note.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Note.Content))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Note.Status))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Note.CreatedByUser.User.FirstName + " " + src.Note.CreatedByUser.User.LastName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Note.CreatedDate));

        CreateMap<UserNoteQueryFilter, NoteQueryServiceFilter>().ReverseMap();
        CreateMap<UserNote, NoteDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Note.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Note.Content))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Note.Status))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Note.CreatedByUser.User.FirstName + " " + src.Note.CreatedByUser.User.LastName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Note.CreatedDate));

        CreateMap<LabelValueResponse, EmployeeRoleKeyValueDTO>().ReverseMap();
        CreateMap<EmployeeRole, EmployeeRoleDTO>();
        CreateMap<LabelValueResponse, B2bGroupRoleDTO>().ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));



/*
        CreateMap<PermissionModel, Handlers.EmployeeRoles.DTOs.PermissionDTO>().ReverseMap();
        CreateMap<PermissionModel, B2BUserPermissionDTO>().ReverseMap();
        CreateMap<ClientPermissionModel, EmployeeRolePermissionsDTO>().ReverseMap();

        CreateMap<KeycloakRoleModel, B2bGroupRoleDTO>().ReverseMap();
        CreateMap<KeycloakRoleModel, KeycloakGroupPermissionDTO>()
            .ForMember(dest => dest.clientId, opt => opt.MapFrom(src => src.containerId));
        CreateMap<KeycloakRoleModel, KeycloakGroupPermissionDTO>().ReverseMap()
            .ForMember(dest => dest.containerId, opt => opt.MapFrom(src => src.clientId));


        CreateMap<ClientPermissionModel, B2BRolePermissionsDTO>()
            .ForMember(dest => dest.clientId, opt => opt.MapFrom(src => src.containerId));

        CreateMap<ClientPermissionModel, GetBusinessPermissionsResponseDto>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.containerId))
            .ForMember(dest => dest.Application, opt => opt.MapFrom(src => src.application));


        CreateMap<ClientPermissionModel, B2BUserRolePermissionDTO>().ReverseMap();
        CreateMap<KeycloakGroupModel, B2bGroupRoleDTO>().ReverseMap();

*/
        #endregion

     
        CreateMap<Sector, SectorDTO>().ReverseMap();
     
        CreateMap<LabelIntValueResponse, SectorDTO>().ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        CreateMap<UserOTP, UserB2C>().ReverseMap()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => (src.PhoneCountryCode + src.Phone)));
      
    }
}