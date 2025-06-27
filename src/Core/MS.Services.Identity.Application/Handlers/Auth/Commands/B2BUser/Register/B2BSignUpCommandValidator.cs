using FluentValidation;
using MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Register;
using MS.Services.Identity.Domain.Enums;
using System.Text.RegularExpressions;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser;

public class B2BSignUpCommandValidator : AbstractValidator<B2BSignUpCommand>
{
    public B2BSignUpCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress()
            .Must(BeValidCompanyEmail).WithMessage("Only company emails are allowed.");
        RuleFor(x => x.PhoneCountryCode).NotEmpty().MaximumLength(8)
            .WithMessage("it must be a maximum of 8 characters."); ;
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(15)
            .WithMessage("it must be a maximum of 15 characters.");
        RuleFor(x => x.TaxNumber).NotEmpty();
        RuleFor(x => x.TaxCertificates).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
             .WithMessage("it must be a minimum of 6 characters.");
        RuleFor(x => x.ConfirmRegisters)
            .NotEmpty().WithMessage("At least one confirm register type must be selected.")
            .Must(ContainExplicitConsent).WithMessage("Explicit consent must be selected.");

        RuleFor(x => x.CountryId).NotEmpty()
           .GreaterThanOrEqualTo(1);

        RuleFor(x => x.CityId).NotEmpty()
            .GreaterThanOrEqualTo(1);


        RuleFor(x => x.AddressName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Address Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.AddressLine1).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Address Line 1 information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.AddressLine2).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Address Line 2 information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.Region).NotEmpty().MaximumLength(64)
            .WithMessage("It cannot be saved without entering Region information. And it must be a maximum of 64 characters.");

        RuleFor(x => x.ZipCode).NotEmpty().MaximumLength(15)
            .WithMessage("It cannot be saved without entering Zip Code information. And it must be a maximum of 15 characters.");


        RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering company information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.SectorId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.NumberOfEmployeeId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.ActivityAreaId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering First Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Last Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.Suffix).NotEmpty().MaximumLength(10)
            .WithMessage("It cannot be saved without entering Suffix information. And it must be a maximum of 10 characters.");

        RuleFor(x => x.PositionId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.OccupationId).NotEmpty()
            .GreaterThanOrEqualTo(1);
    }

    private bool ContainExplicitConsent(List<ConfirmRegisterEnum> confirmRegisters)
    {
        return confirmRegisters != null &&
         confirmRegisters.Contains(ConfirmRegisterEnum.ExplicitConsent) &&
         confirmRegisters.Contains(ConfirmRegisterEnum.KVKKConsent) &&
         confirmRegisters.Count >= 2;
    }

    private bool BeValidCompanyEmail(string email)
    {
        // Geçerli şirket alan adlarını bir dizi olarak belirleyin. TODO: 
        string[] validCompanyDomains = { ".com",".info",".com.tr",".edu.tr"};

        // E-posta adresinden domain'i almak için basit bir Regex kullanıyoruz.
        string domainPattern = @"(?<=@)[^.@]+(?=\.([a-zA-Z]{2,}|[0-9]{1,}))";
        Match match = Regex.Match(email, domainPattern);

        // Eğer e-posta adresinde domain bilgisi bulunamazsa veya
        // şirketin alan adları arasında değilse false döndürüyoruz.
        if (!match.Success /*|| !validCompanyDomains.Contains(match.Value)*/)
        {
            return false;
        }

        // Kişisel e-posta sağlayıcılarının alan adlarını bir dizi olarak belirleyin.
        string[] personalEmailProviders = { "gmail", "hotmail", "yahoo", "outlook" };

        // E-posta adresinden domain'i alıyoruz.
        string domain = match.Value;

        // Eğer e-posta adresi kişisel e-posta sağlayıcılarından birine aitse false döndürüyoruz.
        return !personalEmailProviders.Contains(domain, StringComparer.OrdinalIgnoreCase);
    }
}
