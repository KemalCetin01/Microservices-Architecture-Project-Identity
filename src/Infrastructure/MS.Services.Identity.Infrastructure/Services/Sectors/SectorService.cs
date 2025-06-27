using AutoMapper;
using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.Sectors.Commands;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Exceptions;

namespace MS.Services.Identity.Infrastructure.Services.Sectors;

public class SectorService : ISectorService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly ISectorRepository _sectorRepository;
    private readonly IMapper _mapper;
    private readonly HeaderContext _headerContext;

    public SectorService(IIdentityUnitOfWork identityUnitOfWork, ISectorRepository sectorRepository, IMapper mapper ,
     HeaderContext headerContext)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _sectorRepository = sectorRepository;
        _mapper = mapper;
        _headerContext = headerContext;
    }

    public async Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken)
    {
        return await _sectorRepository.GetFKeyListAsync(cancellationToken);
    }

    public async Task<PagedResponse<SectorDTO>> SearchAsync(SearchQueryModel<SearchSectorsQueryFilterModel> searchQuery, CancellationToken cancellationToken)
    {
        var result = await _sectorRepository.SearchAsync(searchQuery, cancellationToken);
        return _mapper.Map<PagedResponse<SectorDTO>>(result);

    }
    public async Task<SectorDTO> AddAsync(CreateSectorCommand createSectorCommand, CancellationToken cancellationToken)
    {
        await SectorConflictControl(createSectorCommand.Name, null, cancellationToken);

        var sector = new Sector() { Name = createSectorCommand.Name, Status = createSectorCommand.Status };
        await _sectorRepository.AddAsync(sector, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<SectorDTO>(sector);
    }
    public async Task<SectorDTO> UpdateAsync(UpdateSectorCommand updateSectorCommand, CancellationToken cancellationToken)
    {
        await SectorConflictControl(updateSectorCommand.Name, updateSectorCommand.Id, cancellationToken);

        var sector = await _sectorRepository.GetById(updateSectorCommand.Id);
        if (sector == null)
            throw new ValidationException(UserStatusCodes.SectorNotFound.Message, UserStatusCodes.SectorNotFound.StatusCode);

        sector.Name = updateSectorCommand.Name;
        sector.Status = updateSectorCommand.Status;
        _sectorRepository.Update(sector);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<SectorDTO>(sector);

    }
    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var sector = await _sectorRepository.GetById(id);
        if (sector == null)
            throw new ValidationException(UserStatusCodes.SectorNotFound.Message, UserStatusCodes.SectorNotFound.StatusCode);
        sector.IsDeleted = true;
        _sectorRepository.Update(sector);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<bool> SectorConflictControl(string? name, int? id, CancellationToken cancellationToken)
    {
        var isSectorExists = await _sectorRepository.HasSectorExists(name, id, cancellationToken);
        if (isSectorExists)
            throw new ConflictException("Eklemeye/güncellemeye çalıştığınız sektör '" + name + "' bazında zaten mevcut");
        return true;
    }

    public async Task<SectorDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _sectorRepository.GetById(id);
        if (result == null)
            throw new ApiException("Sector Not Found!");

        return _mapper.Map<SectorDTO>(result);
    }
}
