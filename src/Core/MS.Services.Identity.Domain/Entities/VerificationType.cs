using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class VerificationType
{
    public int Id { get; set; }
    public VerificationTypeEnum Name { get; set; }
}
