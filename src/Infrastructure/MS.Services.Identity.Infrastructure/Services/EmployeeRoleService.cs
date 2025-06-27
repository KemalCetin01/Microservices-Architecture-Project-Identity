using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Exceptions;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Services;
public class EmployeeRoleService : IEmployeeRoleService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IEmployeeRoleRepository _employeeRoleRepository;
    private readonly IIdentityEmployeeService _identityEmployeeService;
    private readonly IMapper _mapper;
    private readonly KeycloakOptions _keycloakOptions;

    public EmployeeRoleService(IIdentityUnitOfWork identityUnitOfWork,
        IEmployeeRoleRepository employeeRoleRepository,
        IMapper mapper,
        IOptions<KeycloakOptions> options,
        IIdentityEmployeeService identityEmployeeService)
    {
        _mapper = mapper;
        _identityUnitOfWork = identityUnitOfWork;
        _employeeRoleRepository = employeeRoleRepository;
        _keycloakOptions = options.Value;
        _identityEmployeeService = identityEmployeeService;
    }
    public async Task<EmployeeRoleDTO> GetByIdAsync(Guid Id, CancellationToken cancellationToken  )
    {
        var employeeRole = await GetById(Id, cancellationToken);
        return _mapper.Map<EmployeeRoleDTO>(employeeRole);
    }
    public async Task<EmployeeRoleDTO> AddAsync(CreateEmployeeRoleCommand createEmployeeCommand, CancellationToken cancellationToken  )
    {

        await RoleConflictControl(createEmployeeCommand.Name, null, cancellationToken);
        CreateIdentityRoleRequestDto createIdentityRoleRequestDto = new CreateIdentityRoleRequestDto
        {
            Name = createEmployeeCommand.Name,
            Description = createEmployeeCommand.Description
        };
        
        var rolesUpdated = await _identityEmployeeService.CreateRoleAsync(createIdentityRoleRequestDto, cancellationToken);
        
        if (!rolesUpdated)
            throw new ApiException(EmployeeRoleConstants.EmployeeRoleAddedError);

        var employeeRole = new EmployeeRole()
        {
            Name = createEmployeeCommand.Name,
            DiscountRate = createEmployeeCommand.DiscountRate,
            Description = createEmployeeCommand.Description

        };

        await _employeeRoleRepository.AddAsync(employeeRole);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<EmployeeRoleDTO>(employeeRole);
    }

    public async Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken  )
    {
        var employeeRole = await GetById(Id, cancellationToken);

        bool isDeleted = await _identityEmployeeService.DeleteRoleAsync(employeeRole.Name, cancellationToken);

        if (!isDeleted)
            throw new ApiException(EmployeeRoleConstants.EmployeeRoleDeletedError);

        employeeRole.IsDeleted = true;
        var transaction = await _identityUnitOfWork.CommitAsync(cancellationToken);

        return transaction == transaction
            ? true
            : throw new ApiException(EmployeeRoleConstants.EmployeeRoleDeletedError);
    }

    public async Task<ICollection<EmployeeRoleKeyValueDTO>> SearchFKeyAsync(string search, CancellationToken cancellationToken  )
    {
        return await _employeeRoleRepository.SearchFKeyAsync(search, cancellationToken);

    }

    public async Task<EmployeeRoleDTO> UpdateAsync(UpdateEmployeeRoleCommand updateEmployeeCommand, CancellationToken cancellationToken  )
    {
        var employeeRole = await GetById(updateEmployeeCommand.Id, cancellationToken);
        employeeRole.Description = updateEmployeeCommand.Description;
        employeeRole.DiscountRate = updateEmployeeCommand.DiscountRate;

        await RoleConflictControl(updateEmployeeCommand.Description, updateEmployeeCommand.Id, cancellationToken);
        _employeeRoleRepository.Update(employeeRole);

        var keycloackRoleModel = new KeycloakRoleModel() { name = employeeRole.Name, description = updateEmployeeCommand.Description };
        UpdateIdentityRoleRequestDto updateIdentityRoleRequestDto = new UpdateIdentityRoleRequestDto
        {
            Name = employeeRole.Name,
            Description = employeeRole.Description
        };

        bool rolesUpdated = await _identityEmployeeService.UpdateRoleAsync(updateIdentityRoleRequestDto, cancellationToken);

        if (!rolesUpdated)
            throw new ApiException(EmployeeRoleConstants.EmployeeRoleUpdatedError);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<EmployeeRoleDTO>(employeeRole);
    }

    private async Task<bool> RoleConflictControl(string name, Guid? id, CancellationToken cancellationToken  )
    {
        var isRoleExists = await _employeeRoleRepository.HasRoleAsync(name, id, cancellationToken);
        if (isRoleExists)
            throw new ConflictException(EmployeeRoleConstants.EmployeeRoleConflict);
        return true;
    }

    private async Task<EmployeeRole> GetById(Guid Id, CancellationToken cancellationToken  )
    {
        var employeeRole = await _employeeRoleRepository.GetById(Id, cancellationToken);
        if (employeeRole == null)
            throw new ResourceNotFoundException(EmployeeRoleConstants.EmployeeRoleNotFound);

        return employeeRole;
    }

    public async Task<PagedResponse<EmployeeRoleDTO>> SearchAsync(SearchQueryModel<SearchUserEmployeeRolesQueryFilterModel> searchQuery, CancellationToken cancellationToken  )
    {
        var result = await _employeeRoleRepository.SearchAsync(searchQuery, cancellationToken);

        var mapResult = _mapper.Map<PagedResponse<EmployeeRoleDTO>>(result);

        return mapResult;
    }
}

