using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Core.ExceptionHandling.Exceptions.Common;

namespace MS.Services.Identity.Domain.Exceptions;

public static class UserStatusCodes
{

    public static BaseException UserNotFound = new ApiException(message: "Kullanıcı bulunamadı!", statusCode: "USER_NOT_EXIST");
    public static BaseException UserDeletedError = new ApiException(message: "Kullanıcı silinirken hata oluştu!", statusCode: "USER_DELETED_ERROR");
    public static BaseException EmailAlreadyAdded = new ApiException(message: "Bu email kullanılmaktadır!", statusCode: "EMAIL_ALREADY_ADDED");

    public static BaseException B2CUserAddedError = new ApiException(message: "B2C Kullanıcısı eklerken hata oluştu!", statusCode: "B2C_USER_ADDED_ERROR");
    public static BaseException B2BUserAddedError = new ApiException(message: "B2B Kullanıcısı eklerken hata oluştu!", statusCode: "B2B_USER_ADDED_ERROR");
    public static BaseException BusinessAddedError = new ApiException(message: "Business eklerken hata oluştu!", statusCode: "BUSINESSS_ADDED_ERROR");

    public static BaseException B2CUserUpdatedError = new ApiException(message: "B2C Kullanıcısını güncellerken hata oluştu!", statusCode: "B2C_USER_UPDATED_ERROR");
    public static BaseException B2BUserUpdatedError = new ApiException(message: "B2B Kullanıcısını güncellerken hata oluştu!", statusCode: "B2B_USER_UPDATED_ERROR");

    public static BaseException CurrentAccountNotFound = new ApiException(message: "Cari hesap bulunamadı!", statusCode: "CURRENT_ACCOUNT_NOT_FOUND");
    public static BaseException CurrentAccountConflict = new ApiException(message: "Cari Mevcut!", statusCode: "CURRENT_ACCOUNT_CONFLICT");
    public static BaseException BusinessNotFound = new ApiException(message: "Business bulunamadı!", statusCode: "BUSINESS_NOT_EXIST");

    public static BaseException ActiveUserInRole = new ApiException(message: "Seçilen Role bağlı {0} adet kullanıcı vardır, lütfen önce kullanıcılardan bu bağlantıyı kaldırınız.!", statusCode: "ROLE_DELETED_ERROR");


    public static BaseException KeycloakError = new ApiException(message: "Keycloak Hatası!", statusCode: "KEYCLOAK_ERROR");

    public static BaseException PlatformNotFound = new ApiException(message: "Platform bulunamadı!", statusCode: "PLATFORM_NOT_FOUND");

    public static BaseException AddressAddedError = new ApiException(message: "Adres eklerken hata oluştu!", statusCode: "ADDRESS_ADDED_ERROR");
    public static BaseException AddressUpdatedError = new ApiException(message: "Adres güncellerken hata oluştu!", statusCode: "ADDRESS_UPDATED_ERROR");

    public static BaseException ActivityAreaNotFound = new ApiException(message: "Activity Area Not Found!", statusCode: "ACTIVITY_AREA_NOT_EXIST");
    public static BaseException OccupationNotFound = new ApiException(message: "Occupation Not Found!", statusCode: "JOB_NOT_EXIST");
    public static BaseException PositionNotFound = new ApiException(message: "Position Not Found!", statusCode: "POSITION_NOT_EXIST");
    public static BaseException SectorNotFound = new ApiException(message: "Sector Not Found!", statusCode: "SECTOR_NOT_EXIST");
    public static BaseException NumberOfEmployeeNotFound = new ApiException(message: "Number Of Employee Not Found!", statusCode: "NUMBER_OF_EMPLOYEE_NOT_EXIST");
    public static BaseException CurrentAccountBusinessNotFound = new ApiException(message: "CurrentAccountBusiness Not Found!", statusCode: "CURRENT_ACCOUNT_BUSINESS_NOT_EXIST");
    public static BaseException CurrentAccountBusinessConflict = new ApiException(message: "CurrentAccount Has Exist!", statusCode: "CURRENT_ACCOUNT_EXIST");


    public static BaseException InvalidEmailOrPhone = new ApiException(message: "Invalid Email or Phone!", statusCode: "INVALID_EMAIL_OR_PHONE");
    public static BaseException InvalidEmail = new ApiException(message: "Invalid Email!", statusCode: "INVALID_EMAIL");
    public static BaseException KeycloakLoginError = new ApiException(message: "An error occurred while logging in to Keycloak: Error={0} - Description={1}!", statusCode: "KEYCLOAK_LOGIN_ERROR");
    public static BaseException KeycloakConflictGroup = new ApiException(message: "The Group Name to be added/updated already exists.", statusCode: "KEYCLOAK_CONFLICT_GROUP");


    public static BaseException PhoneConflict= new ApiException(message: "Telefon No Unique Olmalı", statusCode: "CONFLICT");
    public static BaseException EmailConflict= new ApiException(message: "Email Unique Olmalı", statusCode: "CONFLICT");

    public static BaseException BusinessConflict = new ApiException(message: "There is a business named CompanyName registered. Please contact the relevant company administrator.", statusCode: "BUSINESS_CONFLICT");
    public static BaseException VerificationTypeControl = new ApiException(message: "The verification method via phone cannot be selected in countries other than Turkey. CountryId=233 (Türkiye) ", statusCode: "VERIFICATION_METHOD_CONTROL");
    public static BaseException GenerateCodeError = new ApiException(message: "Code could not be generated", statusCode: "GENERATE_CODE_ERROR");
    public static BaseException UserInActive = new ApiException(message: "User is Inactive!", statusCode: "USER_IN_ACTIVE");

}
