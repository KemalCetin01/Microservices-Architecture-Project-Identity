namespace MS.Services.Identity.Domain.EntityConstants;

public static class BusinessConstants
{
    public const int NameMaxLength = 250;
    public const int RoleNameMaxLength = 250;
    public const int CodeMaxLength = 50;
    public const int PhoneMaxLength = 20;
    public const int PhoneCountryCodeMaxLength = 5;
    public const int FaxNumberMaxLength = 20;
    public const int DiscountRateMaxValue = 100;
    public const int DiscountRateMinValue = 0;
    public const string TurkeyCountryCode = "TR";
    
    public static class File
    {
        public const string KVKK = nameof(KVKK);
        public const string TaxPlate = nameof(TaxPlate);
    }
}