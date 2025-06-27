using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Occupations.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Occupations.Commands;

public class UpdateOccupationCommand : ICommand<OccupationDTO>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StatusEnum Status{ get; set; }
}
public sealed class UpdateOccupationCommandHandler : BaseCommandHandler<UpdateOccupationCommand, OccupationDTO>
{
    private readonly IOccupationService _occupationService;

    public UpdateOccupationCommandHandler(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    public override async Task<OccupationDTO> Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
    {
        return await _occupationService.UpdateAsync(request, cancellationToken);
    }
}