using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.Notes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.NoteFilters;

namespace MS.Services.Identity.Infrastructure.Services.Notes;

public class NoteService : INoteService
{
    private readonly IMapper _mapper;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly INoteRepository _noteRepository;
    //private readonly IUserNoteRepository _userNoteRepository;

    public NoteService(IMapper mapper, IIdentityUnitOfWork identityUnitOfWork, INoteRepository noteRepository)
    {
        _mapper = mapper;
        _identityUnitOfWork = identityUnitOfWork;
        _noteRepository = noteRepository;
    }

    public async Task<NoteDTO> AddAsync(CreateNoteCommand model, CancellationToken cancellationToken  )
    {
        var noteEntity = new Note
        {
            Content = model.Content,
            Status = model.Status,
            //NoteType = model.NoteType,
            //RelationID = model.RelationId
        };
        await _noteRepository.AddAsync(noteEntity, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(noteEntity);
    }

    public async Task<PagedResponse<NoteDTO>> GetAllByRelationId(Guid relationId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken  )
    {
        var userNotes = await _noteRepository.GetAllByRelationId(relationId, searchQuery, cancellationToken);

        return _mapper.Map<PagedResponse<NoteDTO>>(userNotes);
    }

    public async Task<NoteDTO> GetById(Guid Id, CancellationToken cancellationToken  )
    {
        var result = await _noteRepository.GetById(Id, cancellationToken);
        return _mapper.Map<NoteDTO>(result);
    }

    public async Task<NoteDTO> Remove(Guid Id, CancellationToken cancellationToken  )
    {
        var existNote = await _noteRepository.GetById(Id, cancellationToken);
        if (existNote != null)
        {
            existNote.IsDeleted = true;
            _noteRepository.Update(existNote);
            await _identityUnitOfWork.CommitAsync(cancellationToken);
            return _mapper.Map<NoteDTO>(existNote);
        }
        return _mapper.Map<NoteDTO>(existNote);
    }

    public async Task<NoteDTO> UpdateAsync(UpdateNoteCommand model, CancellationToken cancellationToken  )
    {
        var currentNote = await _noteRepository.GetById(model.Id, cancellationToken);

        currentNote.Content = model.Content;
        currentNote.Status = model.Status;
        _noteRepository.Update(currentNote);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(currentNote);
    }
}
