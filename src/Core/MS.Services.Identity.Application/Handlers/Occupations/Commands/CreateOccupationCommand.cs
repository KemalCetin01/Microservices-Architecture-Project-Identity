using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Occupations.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Occupations.Commands;

public class CreateOccupationCommand:ICommand<OccupationDTO>
{
    public string Name { get; set; }
    public StatusEnum Status { get; set; }

}
public sealed class CreateOccupationCommandHandler : BaseCommandHandler<CreateOccupationCommand, OccupationDTO>
{
    private readonly IOccupationService _occupationService;

    public CreateOccupationCommandHandler(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }


    public override async Task<OccupationDTO> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        return await _occupationService.AddAsync(request, cancellationToken);
    }
}

