using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.Notes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Application.Handlers.UserNotes.Commands;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Infrastructure.Services.UserNotes;

public class UserNoteService : IUserNoteService
{
    private readonly IMapper _mapper;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IUserNoteRepository _userNoteRepository;
    private readonly INoteRepository _noteRepository;

    public UserNoteService(IMapper mapper, IIdentityUnitOfWork identityUnitOfWork, IUserNoteRepository userNoteRepository, INoteRepository noteRepository)
    {
        _mapper = mapper;
        _identityUnitOfWork = identityUnitOfWork;
        _userNoteRepository = userNoteRepository;
        _noteRepository = noteRepository;
    }

    public async Task<NoteDTO> AddAsync(CreateUserNoteCommand model, CancellationToken cancellationToken  )
    {
        var noteEntity = new Note
        {
            Content = model.Content,
            Status = model.Status,
        };
        await _noteRepository.AddAsync(noteEntity, cancellationToken);

        var userNoteEntity = new UserNote
        {
            NoteId = noteEntity.Id,
            UserId = model.RelationId,
        };

        await _userNoteRepository.AddAsync(userNoteEntity, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(noteEntity);
    }
    public async Task<NoteDTO> UpdateAsync(UpdateUserNoteCommand model, CancellationToken cancellationToken  )
    {
        var currentNote = await _noteRepository.GetById(model.Id, cancellationToken);

        currentNote.Content = model.Content;
        currentNote.Status = model.Status;
        _noteRepository.Update(currentNote);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<NoteDTO>(currentNote);
    }

    public async Task<PagedResponse<NoteDTO>> GetAllByUserNoteId(Guid userNoteId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken  )
    {
        var userNotes = await _userNoteRepository.GetAllByUserId(userNoteId, searchQuery, cancellationToken);

        return _mapper.Map<PagedResponse<NoteDTO>>(userNotes);
    }
}
