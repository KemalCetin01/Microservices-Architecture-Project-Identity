using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Infrastructure.Validators;

public interface IBusinessValidator : IScopedService
{
    Task ValidateBusinessBeforeSave(BaseCreateUpdateBusinessRequestDto baseCreateUpdateBusinessRequestDto, CancellationToken cancellationToken);
}

public class BusinessValidator : IBusinessValidator
{
    private readonly IActivityAreaRepository _activityAreaRepository;
    private readonly INumberOfEmployeeRepository _numberOfEmployeeRepository;
    private readonly ISectorRepository _sectorRepository;
    private readonly IUserEmployeeRepository _userEmployeeRepository;

    public BusinessValidator(IActivityAreaRepository activityAreaRepository,
                           INumberOfEmployeeRepository numberOfEmployeeRepository,
                           ISectorRepository sectorRepository,
                           IUserEmployeeRepository userEmployeeRepository)
    {
        _activityAreaRepository = activityAreaRepository;
        _numberOfEmployeeRepository = numberOfEmployeeRepository;
        _sectorRepository = sectorRepository;
        _userEmployeeRepository = userEmployeeRepository;
    }

    public async Task ValidateBusinessBeforeSave(BaseCreateUpdateBusinessRequestDto baseCreateUpdateBusinessRequestDto, CancellationToken cancellationToken) {
        if (baseCreateUpdateBusinessRequestDto.ActivityAreaId != null) 
        {
            var activityArea = await _activityAreaRepository.GetById(baseCreateUpdateBusinessRequestDto.ActivityAreaId, cancellationToken);
            if (activityArea == null || !activityArea.Status.Equals(StatusEnum.Active)) throw new ValidationException("Activity Area can not be found!");
        }
        if (baseCreateUpdateBusinessRequestDto.SectorId != null) 
        {
            var sector = await _sectorRepository.GetById(baseCreateUpdateBusinessRequestDto.SectorId, cancellationToken);
            if (sector == null || !sector.Status.Equals(StatusEnum.Active)) throw new ValidationException("Sector can not be found!");
        }
        if (baseCreateUpdateBusinessRequestDto.NumberOfEmployeeId != null) 
        {
            var noe = await _numberOfEmployeeRepository.GetById(baseCreateUpdateBusinessRequestDto.NumberOfEmployeeId, cancellationToken);
            if (noe == null || !noe.Status.Equals(StatusEnum.Active)) throw new ValidationException("Number of Employee can not be found!");
        }
        if (baseCreateUpdateBusinessRequestDto.RepresentativeId != null) 
        {
            var representative = await _userEmployeeRepository.GetById(baseCreateUpdateBusinessRequestDto.RepresentativeId, cancellationToken);
            if (representative == null || representative.IsDeleted) throw new ValidationException("Representative can not be found!");
        }
    }
}