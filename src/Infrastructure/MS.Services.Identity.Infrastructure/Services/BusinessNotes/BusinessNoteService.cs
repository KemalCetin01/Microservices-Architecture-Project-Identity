using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.Notes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Services.Identity.Application.Handlers.BusinessNotes.Commands;

namespace MS.Services.Identity.Infrastructure.Services.BusinessNotes;

public class BusinessNoteService : IBusinessNoteService
{
    private readonly IMapper _mapper;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IBusinessNoteRepository _businessNoteRepository;
    private readonly INoteRepository _noteRepository;

    public BusinessNoteService(IMapper mapper, IIdentityUnitOfWork identityUnitOfWork, IBusinessNoteRepository businessNoteRepository, INoteRepository noteRepository)
    {
        _mapper = mapper;
        _identityUnitOfWork = identityUnitOfWork;
        _businessNoteRepository = businessNoteRepository;
        _noteRepository = noteRepository;
    }

    public async Task<NoteDTO> AddAsync(CreateBusinessNoteCommand model, CancellationToken cancellationToken  )
    {
        var noteEntity = new Note
        {
            Content = model.Content,
            Status = model.Status,
        };
        await _noteRepository.AddAsync(noteEntity, cancellationToken);

        var businessNoteEntity = new BusinessNote
        {
            NoteId = noteEntity.Id,
            BusinessId = model.RelationId,
        };

        await _businessNoteRepository.AddAsync(businessNoteEntity, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(noteEntity);
    }
    public async Task<NoteDTO> UpdateAsync(UpdateBusinessNoteCommand model, CancellationToken cancellationToken  )
    {
        var currentNote = await _noteRepository.GetById(model.Id, cancellationToken);

        currentNote.Content = model.Content;
        currentNote.Status = model.Status;
        _noteRepository.Update(currentNote);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(currentNote);
    }

    public async Task<PagedResponse<NoteDTO>> GetAllByBusinessNoteId(Guid businessNoteId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken  )
    {
        var businessNotes = await _businessNoteRepository.GetAllByBusinessId(businessNoteId, searchQuery, cancellationToken);

        return _mapper.Map<PagedResponse<NoteDTO>>(businessNotes);
    }
}
