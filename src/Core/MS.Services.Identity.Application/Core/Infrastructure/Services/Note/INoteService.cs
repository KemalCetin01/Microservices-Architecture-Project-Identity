using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.Notes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Domain.Filters.NoteFilters;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface INoteService : IScopedService
{
    Task<NoteDTO> GetById(Guid Id, CancellationToken cancellationToken  );
    Task<NoteDTO> Remove(Guid Id, CancellationToken cancellationToken  );
}
