using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Application.DTOs.AddressLocation.Response;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.EntityConstants;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public sealed class CreateOrUpdateBusinessBillingAddressCommand : CreateOrUpdateBusinessBillingAddressRequestDto, ICommand<GetBusinessBillingAddressResponseDto>
{
}

public sealed class CreateOrUpdateBusinessBillingAddressCommandHandler : BaseCommandHandler<CreateOrUpdateBusinessBillingAddressCommand, GetBusinessBillingAddressResponseDto>
{
    private readonly IBusinessBillingAddressService _businessBillingAddressService;

    public CreateOrUpdateBusinessBillingAddressCommandHandler(IBusinessBillingAddressService businessBillingAddressService)
    {
        _businessBillingAddressService = businessBillingAddressService;
    }

    public override async Task<GetBusinessBillingAddressResponseDto> Handle(CreateOrUpdateBusinessBillingAddressCommand request, CancellationToken cancellationToken)
    {
        return await _businessBillingAddressService.CreateOrUpdateAsync(request, cancellationToken);
    }
}


public sealed class CreateOrUpdateBusinessBillingAddressCommandValidator : AbstractValidator<CreateOrUpdateBusinessBillingAddressCommand>
{
    public CreateOrUpdateBusinessBillingAddressCommandValidator()
    {
        RuleFor(x => x.BillingType).NotEmpty();
        RuleFor(x => x.BusinessId).NotEmpty();
        RuleFor(x => x.Email).MaximumLength(BaseAddressConstants.EmailMaxLength).NotEmpty();
        RuleFor(x => x.FirstName).MaximumLength(BaseAddressConstants.FirstNameMaxLength);
        RuleFor(x => x.LastName).MaximumLength(BaseAddressConstants.LastNameMaxLength);
        RuleFor(x => x.CompanyName).MaximumLength(BaseAddressConstants.CompanyNameMaxLength);
        RuleFor(x => x.IdentityNumber).MaximumLength(BaseAddressConstants.IdentityNumberMaxLength);
        RuleFor(x => x.TaxNumber).MaximumLength(BaseAddressConstants.TaxNumberMaxLength);
        RuleFor(x => x.TaxOffice).MaximumLength(BaseAddressConstants.TaxOfficeMaxLength);
        RuleFor(x => x.PhoneNumber).MaximumLength(BaseAddressConstants.PhoneNumberMaxLength);
    }
}