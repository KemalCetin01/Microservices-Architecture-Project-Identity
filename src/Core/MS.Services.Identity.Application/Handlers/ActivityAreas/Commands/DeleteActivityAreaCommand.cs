using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.ActivityAreas.Commands;

public class DeleteActivityAreaCommand : ICommand
{
    public int Id { get; set; }
}
public sealed class DeleteActivityAreaCommandHandler : BaseCommandHandler<DeleteActivityAreaCommand>
{
    private readonly IActivityAreaService _activityAreaService;

    public DeleteActivityAreaCommandHandler(IActivityAreaService activityAreaService)
    {
        _activityAreaService = activityAreaService;
    }


    public override async Task Handle(DeleteActivityAreaCommand request, CancellationToken cancellationToken)
    {
        await _activityAreaService.DeleteAsync(request.Id, cancellationToken);
    }
}
