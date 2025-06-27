using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.ActivityAreas.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.ActivityAreas.Commands;

public class CreateActivityAreaCommand:ICommand<ActivityAreaDTO>
{
    public string Name { get; set; }
    public StatusEnum Status { get; set; }

}
public sealed class CreateActivityAreaCommandHandler : BaseCommandHandler<CreateActivityAreaCommand, ActivityAreaDTO>
{
    private readonly IActivityAreaService _activityAreaService;

    public CreateActivityAreaCommandHandler(IActivityAreaService activityAreaService)
    {
        _activityAreaService = activityAreaService;
    }


    public override async Task<ActivityAreaDTO> Handle(CreateActivityAreaCommand request, CancellationToken cancellationToken)
    {
        return await _activityAreaService.AddAsync(request, cancellationToken);
    }
}

