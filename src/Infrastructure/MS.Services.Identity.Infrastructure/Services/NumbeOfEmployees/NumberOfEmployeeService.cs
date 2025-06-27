using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Commands;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.DTOs;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Infrastructure.Services.NumbeOfEmployees;

public class NumberOfEmployeeService : INumberOfEmployeeService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly INumberOfEmployeeRepository _NumberOfEmployeeRepository;
    private readonly IMapper _mapper;

    public NumberOfEmployeeService(IIdentityUnitOfWork identityUnitOfWork, INumberOfEmployeeRepository NumberOfEmployeeRepository, IMapper mapper)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _NumberOfEmployeeRepository = NumberOfEmployeeRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken)
    {
        return await _NumberOfEmployeeRepository.GetFKeyListAsync(cancellationToken);
    }

    public async Task<PagedResponse<NumberOfEmployeeDTO>> SearchAsync(SearchQueryModel<SearchNumberOfEmployeeQueryFilterModel> searchQuery, CancellationToken cancellationToken)
    {
        var result = await _NumberOfEmployeeRepository.SearchAsync(searchQuery, cancellationToken);
        var mapResult = _mapper.Map<PagedResponse<NumberOfEmployeeDTO>>(result);

        return mapResult;
    }
    public async Task<NumberOfEmployeeDTO> AddAsync(CreateNumberOfEmployeeCommand createNumberOfEmployeeCommand, CancellationToken cancellationToken)
    {
        await NumberOfEmployeeConflictControl(createNumberOfEmployeeCommand.Name, null, cancellationToken);

        var NumberOfEmployee = new NumberOfEmployee() { Name = createNumberOfEmployeeCommand.Name, Status = createNumberOfEmployeeCommand.Status };
        await _NumberOfEmployeeRepository.AddAsync(NumberOfEmployee, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NumberOfEmployeeDTO>(NumberOfEmployee);
    }
    public async Task<NumberOfEmployeeDTO> UpdateAsync(UpdateNumberOfEmployeeCommand updateNumberOfEmployeeCommand, CancellationToken cancellationToken)
    {
        await NumberOfEmployeeConflictControl(updateNumberOfEmployeeCommand.Name, updateNumberOfEmployeeCommand.Id, cancellationToken);

        var NumberOfEmployee = await _NumberOfEmployeeRepository.GetById(updateNumberOfEmployeeCommand.Id);
        if (NumberOfEmployee == null)
            throw new ValidationException(UserStatusCodes.NumberOfEmployeeNotFound.Message, UserStatusCodes.NumberOfEmployeeNotFound.StatusCode);

        NumberOfEmployee.Name = updateNumberOfEmployeeCommand.Name;
        NumberOfEmployee.Status = updateNumberOfEmployeeCommand.Status;
        _NumberOfEmployeeRepository.Update(NumberOfEmployee);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<NumberOfEmployeeDTO>(NumberOfEmployee);

    }
    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var NumberOfEmployee = await _NumberOfEmployeeRepository.GetById(id);
        if (NumberOfEmployee == null)
            throw new ValidationException(UserStatusCodes.NumberOfEmployeeNotFound.Message, UserStatusCodes.NumberOfEmployeeNotFound.StatusCode);
        NumberOfEmployee.IsDeleted = true;
        _NumberOfEmployeeRepository.Update(NumberOfEmployee);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<bool> NumberOfEmployeeConflictControl(string? name, int? id, CancellationToken cancellationToken)
    {
        var isNumberOfEmployeeExists = await _NumberOfEmployeeRepository.HasNumberOfEmployeeExists(name, id, cancellationToken);
        if (isNumberOfEmployeeExists)
            throw new ConflictException("Eklemeye/güncellemeye çalıştığınız sektör '" + name + "' bazında zaten mevcut");
        return true;
    }

    public async Task<NumberOfEmployeeDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _NumberOfEmployeeRepository.GetById(id);

        if (result == null)
            throw new ApiException("Number of Employee Not Found!");
        return _mapper.Map<NumberOfEmployeeDTO>(result);
    }
}
