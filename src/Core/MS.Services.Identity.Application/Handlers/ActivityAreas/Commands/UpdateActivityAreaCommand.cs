using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.ActivityAreas.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.ActivityAreas.Commands;

public class UpdateActivityAreaCommand : ICommand<ActivityAreaDTO>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StatusEnum Status{ get; set; }
}
public sealed class UpdateActivityAreaCommandHandler : BaseCommandHandler<UpdateActivityAreaCommand, ActivityAreaDTO>
{
    private readonly IActivityAreaService _activityAreaService;

    public UpdateActivityAreaCommandHandler(IActivityAreaService activityAreaService)
    {
        _activityAreaService = activityAreaService;
    }

    public override async Task<ActivityAreaDTO> Handle(UpdateActivityAreaCommand request, CancellationToken cancellationToken)
    {
        return await _activityAreaService.UpdateAsync(request, cancellationToken);
    }
}