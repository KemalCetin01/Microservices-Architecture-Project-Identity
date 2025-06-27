using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.CurrentAccountNotes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface ICurrentAccountNoteService : IScopedService
{
    Task<NoteDTO> AddAsync(CreateCurrentAccountNoteCommand model, CancellationToken cancellationToken  );
    Task<NoteDTO> UpdateAsync(UpdateCurrentAccountNoteCommand model, CancellationToken cancellationToken  );
    Task<PagedResponse<NoteDTO>> GetAllByCurrentAccountId(Guid currentAccountId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken  );
}
