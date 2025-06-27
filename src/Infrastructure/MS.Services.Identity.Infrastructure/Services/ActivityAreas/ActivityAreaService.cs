using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.ActivityAreas.Commands;
using MS.Services.Identity.Application.Handlers.ActivityAreas.DTOs;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Exceptions;

namespace MS.Services.Identity.Infrastructure.Services.ActivityAreas;

public class ActivityAreaService : IActivityAreaService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IActivityAreaRepository _activityAreaRepository;
    private readonly IMapper _mapper;

    public ActivityAreaService(IIdentityUnitOfWork identityUnitOfWork, IActivityAreaRepository activityAreaRepository, IMapper mapper)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _activityAreaRepository = activityAreaRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken  )
    {
        return await _activityAreaRepository.GetFKeyListAsync(cancellationToken);
    }

    public async Task<PagedResponse<ActivityAreaDTO>> SearchAsync(SearchQueryModel<SearchActivityAreasQueryFilterModel> searchQuery, CancellationToken cancellationToken  )
    {
        var result = await _activityAreaRepository.SearchAsync(searchQuery, cancellationToken);
        var mapResult = _mapper.Map<PagedResponse<ActivityAreaDTO>>(result);

        return mapResult;
    }
    public async Task<ActivityAreaDTO> AddAsync(CreateActivityAreaCommand createActivityAreaCommand, CancellationToken cancellationToken  )
    {
        await ActivityAreaConflictControl(createActivityAreaCommand.Name, null, cancellationToken);

        var activityArea = new ActivityArea() { Name = createActivityAreaCommand.Name, Status = createActivityAreaCommand.Status };
        await _activityAreaRepository.AddAsync(activityArea, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<ActivityAreaDTO>(activityArea);
    }
    public async Task<ActivityAreaDTO> UpdateAsync(UpdateActivityAreaCommand updateActivityAreaCommand, CancellationToken cancellationToken  )
    {
        await ActivityAreaConflictControl(updateActivityAreaCommand.Name, updateActivityAreaCommand.Id, cancellationToken);

        var activityArea = await _activityAreaRepository.GetById(updateActivityAreaCommand.Id);
        if (activityArea == null)
            throw new ValidationException(UserStatusCodes.ActivityAreaNotFound.Message, UserStatusCodes.ActivityAreaNotFound.StatusCode);

        activityArea.Name = updateActivityAreaCommand.Name;
        activityArea.Status = updateActivityAreaCommand.Status;
        _activityAreaRepository.Update(activityArea);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<ActivityAreaDTO>(activityArea);

    }
    public async Task DeleteAsync(int id, CancellationToken cancellationToken  )
    {
        var activityArea = await _activityAreaRepository.GetById(id);
        if (activityArea == null)
            throw new ValidationException(UserStatusCodes.ActivityAreaNotFound.Message, UserStatusCodes.ActivityAreaNotFound.StatusCode);
        activityArea.IsDeleted = true;
        _activityAreaRepository.Remove(activityArea);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<bool> ActivityAreaConflictControl(string? name, int? id, CancellationToken cancellationToken  )
    {
        var isActivityAreaExists = await _activityAreaRepository.HasActivityAreaExists(name, id, cancellationToken);
        if (isActivityAreaExists)
            throw new ConflictException("Eklemeye/güncellemeye çalıştığınız aktivite alanı '" + name + "' bazında zaten mevcut");
        return true;
    }

    public async Task<ActivityAreaDTO> GetByIdAsync(int id, CancellationToken cancellationToken  )
    {
        var result = await _activityAreaRepository.GetById(id);

        if (result == null)
            throw new ApiException("Activity Area Not Found!");
        return _mapper.Map<ActivityAreaDTO>(result);
    }
}
