using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.EntityConstants;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public sealed class CreateBusinessCommand : ICommand<GetBusinessResponseDto>
{
    public CreateBusinessRequestDTO GeneralInfo { get; set; } = null!;
    public CreateOrUpdateBusinessBillingAddressRequestDto BillingAddress { get; set; } = null!;
}

public sealed class CreateBusinessCommandHandler : BaseCommandHandler<CreateBusinessCommand, GetBusinessResponseDto>
{
    private readonly IBusinessService _businessService;
    private readonly IBusinessBillingAddressService _businessBillingAddressService;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;

    public CreateBusinessCommandHandler(IBusinessService businessService, IBusinessBillingAddressService businessBillingAddressService, IIdentityUnitOfWork identityUnitOfWork)
    {
        _businessService = businessService;
        _businessBillingAddressService = businessBillingAddressService;
        _identityUnitOfWork = identityUnitOfWork;
    }

    public override async Task<GetBusinessResponseDto> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {
        await _identityUnitOfWork.BeginTransactionAsync(cancellationToken);
        GetBusinessResponseDto businessResponseDto = await _businessService.CreateAsync(request.GeneralInfo, cancellationToken);
        request.BillingAddress.BusinessId = businessResponseDto.Id;

        businessResponseDto.BillingAddress = await _businessBillingAddressService.CreateOrUpdateAsync(request.BillingAddress, cancellationToken);

        await _identityUnitOfWork.CommitTransactionAsync(cancellationToken);
        return businessResponseDto;

    }
}

public sealed class CreateBusinessCommandValidator : AbstractValidator<CreateBusinessCommand>
{
    public CreateBusinessCommandValidator()
    {
        RuleFor(x => x.GeneralInfo)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(x => x.GeneralInfo.Name).MaximumLength(BusinessConstants.NameMaxLength).NotEmpty();
                RuleFor(x => x.GeneralInfo.Phone).MaximumLength(BusinessConstants.PhoneMaxLength);
                RuleFor(x => x.GeneralInfo.PhoneCountryCode).MaximumLength(BusinessConstants.PhoneCountryCodeMaxLength);
                RuleFor(x => x.GeneralInfo.FaxNumber).MaximumLength(BusinessConstants.FaxNumberMaxLength);
                RuleFor(x => x.GeneralInfo.BusinessStatus).NotEmpty();
                RuleFor(x => x.GeneralInfo.DiscountRate).InclusiveBetween(BusinessConstants.DiscountRateMinValue, BusinessConstants.DiscountRateMaxValue)
                    .WithMessage("The value must be between 0 and 100.");
            });
        
        RuleFor(x => x.BillingAddress)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(x => x.BillingAddress.Email).MaximumLength(BaseAddressConstants.EmailMaxLength).NotEmpty();
                RuleFor(x => x.BillingAddress.FirstName).MaximumLength(BaseAddressConstants.FirstNameMaxLength);
                RuleFor(x => x.BillingAddress.LastName).MaximumLength(BaseAddressConstants.LastNameMaxLength);
                RuleFor(x => x.BillingAddress.CompanyName).MaximumLength(BaseAddressConstants.CompanyNameMaxLength);
                RuleFor(x => x.BillingAddress.IdentityNumber).MaximumLength(BaseAddressConstants.IdentityNumberMaxLength);
                RuleFor(x => x.BillingAddress.TaxNumber).MaximumLength(BaseAddressConstants.TaxNumberMaxLength);
                RuleFor(x => x.BillingAddress.TaxOffice).MaximumLength(BaseAddressConstants.TaxOfficeMaxLength);
                RuleFor(x => x.BillingAddress.PhoneNumber).MaximumLength(BaseAddressConstants.PhoneNumberMaxLength);
            });
    }
}