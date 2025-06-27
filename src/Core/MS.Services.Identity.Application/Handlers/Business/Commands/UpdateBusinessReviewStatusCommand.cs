using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public sealed class UpdateBusinessReviewStatusCommand : UpdateBusinessReviewStatusRequestDto, ICommand<UpdateBusinessReviewStatusResponseDto>
{
}

public sealed class UpdateBusinessReviewStatusCommandHandler : BaseCommandHandler<UpdateBusinessReviewStatusCommand, UpdateBusinessReviewStatusResponseDto>
{
    private readonly IBusinessService _businessService;
    public UpdateBusinessReviewStatusCommandHandler(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    public override async Task<UpdateBusinessReviewStatusResponseDto> Handle(UpdateBusinessReviewStatusCommand request, CancellationToken cancellationToken)
    {
        return await _businessService.UpdateReviewStatusAsync(request, cancellationToken);
    }
}


public sealed class UpdateBusinessReviewStatusCommandValidator : AbstractValidator<UpdateBusinessReviewStatusCommand>
{
    public UpdateBusinessReviewStatusCommandValidator()
    {
        RuleFor(x => x.ReviewStatus).IsInEnum().NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
    }
}
