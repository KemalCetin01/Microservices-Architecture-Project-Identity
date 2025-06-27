using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.Positions.Commands;
using MS.Services.Identity.Application.Handlers.Positions.DTOs;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Exceptions;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Models.FKeyModel;

namespace MS.Services.Identity.Infrastructure.Services.Positions;

public class PositionService : IPositionService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;

    public PositionService(IIdentityUnitOfWork identityUnitOfWork, IPositionRepository positionRepository, IMapper mapper)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken  )
    {
        return await _positionRepository.GetFKeyListAsync(cancellationToken);
    }

    public async Task<PagedResponse<PositionDTO>> SearchAsync(SearchQueryModel<SearchPositionsQueryFilterModel> searchQuery, CancellationToken cancellationToken  )
    {
       var result= await _positionRepository.SearchAsync(searchQuery, cancellationToken);
        var mapResult = _mapper.Map<PagedResponse<PositionDTO>>(result);

        return mapResult;
    }
    public async Task<PositionDTO> AddAsync(CreatePositionCommand createPositionCommand, CancellationToken cancellationToken  )
    {
        await PositionConflictControl(createPositionCommand.Name,null, cancellationToken);

        var position=new Position() { Name = createPositionCommand.Name,Status=createPositionCommand.Status };
        await _positionRepository.AddAsync(position,cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<PositionDTO>(position);
    }
    public async Task<PositionDTO> UpdateAsync(UpdatePositionCommand updatePositionCommand, CancellationToken cancellationToken  )
    {
        await PositionConflictControl(updatePositionCommand.Name, updatePositionCommand.Id, cancellationToken);

        var position = await _positionRepository.GetById(updatePositionCommand.Id);
        if (position == null)
            throw new ValidationException(UserStatusCodes.PositionNotFound.Message, UserStatusCodes.PositionNotFound.StatusCode);

        position.Name= updatePositionCommand.Name;
        position.Status = updatePositionCommand.Status;
         _positionRepository.Update(position);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<PositionDTO>(position);

    }
    public async Task DeleteAsync(int id, CancellationToken cancellationToken  )
    {
        var position=await _positionRepository.GetById(id);
        if (position == null)
            throw new ValidationException(UserStatusCodes.PositionNotFound.Message, UserStatusCodes.PositionNotFound.StatusCode);
        position.IsDeleted= true;
        _positionRepository.Update(position);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<bool> PositionConflictControl(string? name,int? id, CancellationToken cancellationToken  )
    {
        var isPositionExists = await _positionRepository.HasPositionExists(name,id, cancellationToken);
        if (isPositionExists)
            throw new ConflictException("Eklemeye/güncellemeye çalıştığınız Position '" + name + "' bazında zaten mevcut");
        return true;
    }

    public async Task<PositionDTO> GetByIdAsync(int id, CancellationToken cancellationToken  )
    {
        var result = await _positionRepository.GetById(id);

        if (result == null)
            throw new ApiException("Position Not Found!");
        return _mapper.Map<PositionDTO>(result);
    }
}
