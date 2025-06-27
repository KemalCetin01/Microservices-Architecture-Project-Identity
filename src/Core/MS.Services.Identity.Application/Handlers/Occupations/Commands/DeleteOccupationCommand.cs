using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Occupations.Commands;

public class DeleteOccupationCommand : ICommand
{
    public int Id { get; set; }
}
public sealed class DeleteOccupationCommandHandler : BaseCommandHandler<DeleteOccupationCommand>
{
    private readonly IOccupationService _occupationService;

    public DeleteOccupationCommandHandler(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }


    public override async Task Handle(DeleteOccupationCommand request, CancellationToken cancellationToken)
    {
        await _occupationService.DeleteAsync(request.Id, cancellationToken);
    }
}
