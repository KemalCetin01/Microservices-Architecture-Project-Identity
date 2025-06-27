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
using MS.Services.Identity.Application.Handlers.CurrentAccountNotes.Commands;

namespace MS.Services.Identity.Infrastructure.Services.CurrentAccountNotes;

public class CurrentAccountNoteService : ICurrentAccountNoteService
{
    private readonly IMapper _mapper;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly ICurrentAccountNoteRepository _currentAccountNoteRepository;
    private readonly INoteRepository _noteRepository;

    public CurrentAccountNoteService(IMapper mapper, IIdentityUnitOfWork identityUnitOfWork, ICurrentAccountNoteRepository currentAccountNoteRepository, INoteRepository noteRepository)
    {
        _mapper = mapper;
        _identityUnitOfWork = identityUnitOfWork;
        _currentAccountNoteRepository = currentAccountNoteRepository;
        _noteRepository = noteRepository;
    }

    public async Task<NoteDTO> AddAsync(CreateCurrentAccountNoteCommand model, CancellationToken cancellationToken  )
    {
        var noteEntity = new Note
        {
            Content = model.Content,
            Status = model.Status,
        };
        await _noteRepository.AddAsync(noteEntity, cancellationToken);

        var currentAccountNoteEntity = new CurrentAccountNote
        {
            NoteId = noteEntity.Id,
            CurrentAccountId = model.RelationId,
        };

        await _currentAccountNoteRepository.AddAsync(currentAccountNoteEntity, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(noteEntity);
    }
    public async Task<NoteDTO> UpdateAsync(UpdateCurrentAccountNoteCommand model, CancellationToken cancellationToken  )
    {
        var currentNote = await _noteRepository.GetById(model.Id, cancellationToken);

        currentNote.Content = model.Content;
        currentNote.Status = model.Status;
        _noteRepository.Update(currentNote);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(currentNote);
    }

    public async Task<PagedResponse<NoteDTO>> GetAllByCurrentAccountId(Guid currentAccountNoteId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken  )
    {
        var currentAccountNotes = await _currentAccountNoteRepository.GetAllByCurrentAccountId(currentAccountNoteId, searchQuery, cancellationToken);

        return _mapper.Map<PagedResponse<NoteDTO>>(currentAccountNotes);
    }
}
