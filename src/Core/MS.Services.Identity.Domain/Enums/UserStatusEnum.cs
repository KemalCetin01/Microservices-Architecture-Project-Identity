namespace MS.Services.Identity.Domain.Enums;

public enum UserStatusEnum
{
    Active = 1,
    Inactive = 2, //Kullanıcının hesabının dondurulması
    Deleted = 3 //Kullanıcının hesabının soft delete ile silinmesi
}
