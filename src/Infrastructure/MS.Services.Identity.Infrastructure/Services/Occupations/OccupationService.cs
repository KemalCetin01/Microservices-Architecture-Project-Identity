using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.Occupations.Commands;
using MS.Services.Identity.Application.Handlers.Occupations.DTOs;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Exceptions;

namespace MS.Services.Identity.Infrastructure.Services.Occupations;

public class OccupationService : IOccupationService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IOccupationRepository _occupationRepository;
    private readonly IMapper _mapper;

    public OccupationService(IIdentityUnitOfWork identityUnitOfWork, IOccupationRepository occupationRepository, IMapper mapper)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _occupationRepository = occupationRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken)
    {
        return await _occupationRepository.GetFKeyListAsync(cancellationToken);
    }

    public async Task<PagedResponse<OccupationDTO>> SearchAsync(SearchQueryModel<SearchOccupationsQueryFilterModel> searchQuery, CancellationToken cancellationToken  )
    {
       var result= await _occupationRepository.SearchAsync(searchQuery, cancellationToken);
        var mapResult = _mapper.Map<PagedResponse<OccupationDTO>>(result);

        return mapResult;
    }
    public async Task<OccupationDTO> AddAsync(CreateOccupationCommand createOccupationCommand, CancellationToken cancellationToken  )
    {
        await OccupationConflictControl(createOccupationCommand.Name,null, cancellationToken);

        var occupation=new Occupation() { Name = createOccupationCommand.Name,Status=createOccupationCommand.Status };
        await _occupationRepository.AddAsync(occupation,cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<OccupationDTO>(occupation);
    }
    public async Task<OccupationDTO> UpdateAsync(UpdateOccupationCommand updateOccupationCommand, CancellationToken cancellationToken  )
    {
        await OccupationConflictControl(updateOccupationCommand.Name, updateOccupationCommand.Id, cancellationToken);

        var occupation = await _occupationRepository.GetById(updateOccupationCommand.Id);
        if (occupation == null)
            throw new ValidationException(UserStatusCodes.OccupationNotFound.Message, UserStatusCodes.OccupationNotFound.StatusCode);

        occupation.Name= updateOccupationCommand.Name;
        occupation.Status = updateOccupationCommand.Status;
         _occupationRepository.Update(occupation);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<OccupationDTO>(occupation);

    }
    public async Task DeleteAsync(int id, CancellationToken cancellationToken  )
    {
        var occupation=await _occupationRepository.GetById(id);
        if (occupation == null)
            throw new ValidationException(UserStatusCodes.OccupationNotFound.Message, UserStatusCodes.OccupationNotFound.StatusCode);
        occupation.IsDeleted= true;
        _occupationRepository.Update(occupation);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<bool> OccupationConflictControl(string? name,int? id, CancellationToken cancellationToken  )
    {
        var isOccupationExists = await _occupationRepository.HasOccupationExists(name,id, cancellationToken);
        if (isOccupationExists)
            throw new ConflictException("Eklemeye/güncellemeye çalıştığınız Occupation '" + name + "' bazında zaten mevcut");
        return true;
    }

    public async Task<OccupationDTO> GetByIdAsync(int id, CancellationToken cancellationToken  )
    {
        var result = await _occupationRepository.GetById(id);

        if (result == null)
            throw new ApiException("Occupation Not Found!");
        return _mapper.Map<OccupationDTO>(result);
    }
}
