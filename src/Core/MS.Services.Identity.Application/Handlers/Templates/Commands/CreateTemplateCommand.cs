using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Handlers.Templates.DTOs;

namespace MS.Services.Identity.Application.Handlers.Templates.Commands;

public sealed class CreateTemplateCommand : ICommand<TemplateResponse>
{

}

public sealed class CreateTemplateCommandHandler : ICommandHandler<CreateTemplateCommand, TemplateResponse>
{
    public Task<TemplateResponse> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class CreateTemplateCommandValidator
{

}
