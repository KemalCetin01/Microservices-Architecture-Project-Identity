using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Exceptions;
using MS.Services.Identity.Domain.Filters.UserB2CFilters;
using Serilog;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Services;

public class UserB2CService : IUserB2CService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserB2CRepository _userB2CRepository;
    private readonly IUserEmployeeRepository _userEmployeeRepository;
    private readonly IBusinessRepository _businessRepository;
    private readonly ICurrentAccountRepository _currentAccountRepository;
    private readonly IMapper _mapper;
    private readonly IBusinessUserRepository _businessUserRepository;
    private readonly IIdentityB2CService _identityB2CService;
    private readonly KeycloakOptions _keycloakOptions;

    private static readonly Serilog.ILogger Logger = Log.ForContext<UserB2CService>();

    public UserB2CService(IIdentityUnitOfWork identityUnitOfWork,
                          IUserRepository userRepository,
                          IUserB2CRepository userB2CRepository,
                          IBusinessRepository businessRepository,
                          ICurrentAccountRepository currentAccountRepository,
                          IMapper mapper,
                          IBusinessUserRepository businessUserRepository,
                          IOptions<KeycloakOptions> options,
                          IUserEmployeeRepository userEmployeeRepository,
                          IIdentityB2CService identityB2CService)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _userRepository = userRepository;
        _userB2CRepository = userB2CRepository;
        _businessRepository = businessRepository;
        _currentAccountRepository = currentAccountRepository;
        _mapper = mapper;
        _businessUserRepository = businessUserRepository;
        _keycloakOptions = options.Value;
        _userEmployeeRepository = userEmployeeRepository;
        _identityB2CService = identityB2CService;
    }


    public async Task<B2CUserGetByIdDTO> CreateAsync(CreateB2CUserCommandDTO model, CancellationToken cancellationToken)
    {
        await ValidateCreateB2CUserAsync(model, cancellationToken);

        User userEntity = CreateB2CUserCommandToUser(model);

        CreateIdentityUserRequestDto requestDto = new CreateIdentityUserRequestDto
        {
            UserId = userEntity.Id,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password
        };

        CreateIdentityUserResponseDto identityUserResponseDto = await _identityB2CService.CreateUserAsync(requestDto, cancellationToken);

        if (identityUserResponseDto.IsSuccess == false)
            throw new ValidationException(UserStatusCodes.KeycloakError.Message, UserStatusCodes.KeycloakError.StatusCode);

        userEntity.IdentityRefId = identityUserResponseDto.IdentityRefId;

        UserB2C userB2CEntity = CreateB2CUserCommandToB2CUser(model, userEntity);

        try
        {
            await _userB2CRepository.AddAsync(userB2CEntity, cancellationToken);
            var result = await _identityUnitOfWork.CommitAsync(cancellationToken);
            return _mapper.Map<B2CUserGetByIdDTO>(userB2CEntity);
        }
        catch (Exception)
        {
            bool isDeleted = await _identityB2CService.DeleteUserAsync(identityUserResponseDto.IdentityRefId, cancellationToken);
            if (!isDeleted)
            {
                Logger.ForContext("KeycloakUserId", identityUserResponseDto.IdentityRefId).Error("There is an error when trying to create b2c user. Keycloak user can not be rollbacked...");
            }
            throw;
        }
    }

    public async Task<B2CUserGetByIdDTO> UpdateAsync(UpdateB2CUserCommandDTO model, CancellationToken cancellationToken)
    {
        Logger.ForContext("UserId", model.Id).Information("Existing B2C User will be updated");

        UserB2C existUserB2C = await ValidateUpdateB2CUserAsync(model, cancellationToken);

        UpdateIdentityUserRequestDto updateIdentityUserRequestDto = new UpdateIdentityUserRequestDto
        {
            IdentityRefId = existUserB2C.User.IdentityRefId,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        await _identityB2CService.UpdateUserAsync(updateIdentityUserRequestDto, cancellationToken);

        UpdateIdentityUserRequestDto updateRollbackUserRequestDto = new UpdateIdentityUserRequestDto
        {
            IdentityRefId = existUserB2C.User.IdentityRefId,
            Email = existUserB2C.User.Email,
            FirstName = existUserB2C.User.FirstName,
            LastName = existUserB2C.User.LastName
        };

        UpdateExistingB2CUserWithModel(model, existUserB2C);

        try
        {
            var result = await _identityUnitOfWork.CommitAsync(cancellationToken);
            Logger.ForContext("UserId", model.Id).Information("Existing B2C User is updated");
            return _mapper.Map<B2CUserGetByIdDTO>(existUserB2C);
        }
        catch (Exception)
        {
            await _identityB2CService.UpdateUserAsync(updateRollbackUserRequestDto, cancellationToken);
            //peki keycloakta tekrar exception alırsak ne yapacağız? 
            throw;
        }
    }

    public async Task DeleteAsync(Guid Id, CancellationToken cancellationToken)
    {
        var existUser = await _userB2CRepository.GetById(Id, cancellationToken);
        if (existUser == null)
            throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

        existUser.IsDeleted = true;
        existUser.User.IsDeleted = true;
        _userB2CRepository.Update(existUser);

        bool isDeleted = await _identityB2CService.DeleteUserAsync(existUser.User.IdentityRefId, cancellationToken);

        if (!isDeleted)
            throw new ValidationException(UserStatusCodes.KeycloakError.Message, UserStatusCodes.KeycloakError.StatusCode);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

    }

    private UserB2C CreateB2CUserCommandToB2CUser(CreateB2CUserCommandDTO model, User userEntity)
    {
        return new UserB2C
        {
            User = userEntity,
            PhoneCountryCode = model.PhoneCountryCode,
            Phone = model.Phone,
            CountryId = model.CountryId,
            CityId = model.CityId,
            TownId = model.TownId,
            Gender = model.Gender,
            BirthDate = model.BirthDate,
            OccupationId = model.OccupationId,
            ActivityAreaId = model.ActivityAreaId,
            UserEmployeeId = model.RepresentativeId,
            SectorId = model.SectorId,
            SiteStatus = model.SiteStatus,
            UserStatus = model.UserStatus,
            ContactPermission = model.ContactPermission
        };
    }

    private User CreateB2CUserCommandToUser(CreateB2CUserCommandDTO model)
    {
        return new User
        {
            UserTypeId = (int)UserTypeEnum.B2C,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Suffix = null, //TODO karalastırılınca eklenecek
        };
    }

    private async Task ValidateCreateB2CUserAsync(CreateB2CUserCommandDTO model, CancellationToken cancellationToken)
    {
        bool hasExistsPhone = await _userB2CRepository.AnotherUserHasPhoneAsync(null, model.PhoneCountryCode, model.Phone, cancellationToken);
        if (hasExistsPhone)
            throw new ValidationException(UserStatusCodes.PhoneConflict.Message, UserStatusCodes.PhoneConflict.StatusCode);

        bool hasExistsEmail = await _userRepository.AnotherUserHasEmailAsync(null, UserTypeEnum.B2C, model.Email, cancellationToken);
        if (hasExistsEmail)
            throw new ValidationException(UserStatusCodes.EmailConflict.Message, UserStatusCodes.EmailConflict.StatusCode);

        if (model.RepresentativeId != null)
        {
            var hasExistsRepresentative = await _userEmployeeRepository.GetById(model.RepresentativeId, cancellationToken);
            if (hasExistsRepresentative == null)
                throw new ResourceNotFoundException(EmployeeRoleConstants.EmployeeRoleNotFound);
        }
    }

    public async Task<B2CUserGetByIdDTO> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        var result = await _userB2CRepository.GetById(Id, cancellationToken);
        return _mapper.Map<B2CUserGetByIdDTO>(result);
    }

    public async Task<PagedResponse<B2CUserListDTO>> GetUsersAsync(SearchQueryModel<UserB2CQueryServiceFilter> searchQuery, CancellationToken cancellationToken)
    {
        var users = await _userB2CRepository.GetAll(searchQuery, cancellationToken);

        return _mapper.Map<PagedResponse<B2CUserListDTO>>(users);
    }

    private void UpdateExistingB2CUserWithModel(UpdateB2CUserCommandDTO model, UserB2C existUserB2C)
    {
        existUserB2C.User.FirstName = model.FirstName;
        existUserB2C.User.LastName = model.LastName;
        existUserB2C.User.Email = model.Email;

        existUserB2C.BirthDate = model.BirthDate;
        existUserB2C.Gender = model.Gender;
        existUserB2C.OccupationId = model.OccupationId;
        existUserB2C.PhoneCountryCode = model.PhoneCountryCode;
        existUserB2C.Phone = model.Phone;
        existUserB2C.CountryId = model.CountryId;
        existUserB2C.CityId = model.CityId;
        existUserB2C.TownId = model.TownId;
        existUserB2C.UserEmployeeId = model.RepresentativeId;
        existUserB2C.SectorId = model.SectorId;
        existUserB2C.ActivityAreaId = model.ActivityAreaId;
        existUserB2C.UserStatus = model.UserStatus;
        existUserB2C.SiteStatus = model.SiteStatus;
        existUserB2C.ContactPermission = model.ContactPermission;

        _userB2CRepository.Update(existUserB2C);
    }

    private async Task<UserB2C> ValidateUpdateB2CUserAsync(UpdateB2CUserCommandDTO model, CancellationToken cancellationToken)
    {
        UserB2C existUserB2C = await _userB2CRepository.GetById(model.Id, cancellationToken);
        if (existUserB2C == null || existUserB2C.User == null)
            throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

        bool hasExistsPhone = await _userB2CRepository.AnotherUserHasPhoneAsync(existUserB2C.UserId, model.PhoneCountryCode, model.Phone, cancellationToken);
        if (hasExistsPhone)
            throw new ValidationException(UserStatusCodes.PhoneConflict.Message, UserStatusCodes.PhoneConflict.StatusCode);

        bool hasExistsEmail = await _userRepository.AnotherUserHasEmailAsync(existUserB2C.UserId, UserTypeEnum.B2C, model.Email, cancellationToken);
        if (hasExistsEmail)
            throw new ValidationException(UserStatusCodes.EmailConflict.Message, UserStatusCodes.EmailConflict.StatusCode);

        if (model.RepresentativeId != null)
        {
            var hasExistsRepresentative = await _userEmployeeRepository.GetById(model.RepresentativeId, cancellationToken);
            if (hasExistsRepresentative == null)
                throw new ResourceNotFoundException(EmployeeRoleConstants.EmployeeRoleNotFound);
        }

        return existUserB2C;

    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordCommandDTO model, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetById(model.UserId, cancellationToken);

        if (userEntity == null)
            throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

        UpdateIdentityUserPasswordRequestDto updatePasswordRequestDto = new UpdateIdentityUserPasswordRequestDto
        {
            IdentityRefId = userEntity.IdentityRefId,
            Password = model.Password
        };

        return await _identityB2CService.UpdateUserPasswordAsync(updatePasswordRequestDto, cancellationToken);

    }
}
