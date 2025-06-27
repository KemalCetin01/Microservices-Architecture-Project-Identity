using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Handlers.Templates.DTOs;

namespace MS.Services.Identity.Application.Handlers.Templates.Queries;

public sealed record GetTemplateByIdQuery : IQuery<TemplateResponse>
{

}

public sealed record GetTemplateByIdQueryHandler : IQueryHandler<GetTemplateByIdQuery, TemplateResponse>
{
    public Task<TemplateResponse> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
