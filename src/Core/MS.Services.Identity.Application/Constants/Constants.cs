using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Constants;

public partial class Constants
{
    public class Platform
    {
        public const string B2B = "B2B";
        public const string B2C = "B2C";
    }
    public class B2BUserConstants
    {
        public const string RecordNotFound = "Record Not Found";

        public const string currentAccountName = "currentAccountName";
        public const string currentId = "currentId";
        public const string fullName = "fullName";
        public const string representative = "representative";
        public const string email = "email";
        public const string Id = "id";
        public const string CreatedDate = "createdDate";
    }

    public class B2CUserConstants
    {
        public const string fullName = "fullName";
        public const string representative = "representative";
        public const string email = "email";
        public const string Id = "id";
        public const string CreatedDate = "createdDate";
        public const string Country = "country";
        public const string City = "city";
        public const string Town = "town";
        public const string RecordNotFound = "Record Not Found";
    }

    public class CurrentAccountsConstants
    {
        public const string CurrentAccountsAdded = "Current Account Successfully Added.";
        public const string CurrentAccountsUpdated = "Current Account Başarıyla Güncellendi.";
        public const string CurrentAccountsDeleted = "Current Account Successfully Updated.";
        public const string CurrentAccountsNotExists = "Current Account Not Found.";


        public const string RecordNotFound = "Record Not Found";


        public const string currentAccountName = "currentAccountName";
    }
    public class BusinessesConstants
    {
        public const string BusinessNotExists = "Business Not Found.";
        public const string BusinessIdCanNotBeNull = "BusinessId Can Not Be Null";


        public const string RecordNotFound = "Record Not Found";
    }

    public class EmployeeConstants
    {
        public const string EmployeeAdded = "UserEmployee Successfully Added";
        public const string EmployeeUpdated = "UserEmployee Successfully Updated";
        public const string EmployeeDeleted = "UserEmployee Successfully Deleted.";

        public const string EmployeeAddedError = "An Error Occured While Adding UserEmployee";
        public const string EmployeeDeletedError = "An Error Occured While Deleting UserEmployee";
        public const string EmployeeUpdatedError = "An Error Occured While Updating UserEmployee";
        public const string EmployeeConflict = "UserEmployee Already Exists!";

        public const string EmployeeCountDiff = "The number of parameters in the selected message template does not match the number of parameters sent.";

        public const string EmployeeNotFound = "UserEmployee Not Found";
        public const string EmployeeIdCanNotBeNullOrEmpty = "UserEmployee ID cannot be null or empty";

        public const string FullName = "fullName";
        public const string PhoneNumber = "phoneNumber";
        public const string Role = "role";
        public const string DiscountRate = "discountRate";
        public const string Email = "email";
        public const string LastDateEntry = "lastDateEntry";
    }
    public class EmployeeManagerConstants
    {

        public const string EmployeeManagerAssignYourselfError = "You cannot appoint an administrator to yourself";

        public const string ErrorWhenDeleted = "Error When Deleted";

    }
    public class EmployeeRoleConstants
    {

        public const string EmployeeRoleAddedError = "An Error Occured While Adding UserEmployee Role";
        public const string EmployeeRoleDeletedError = "An Error Occured While Deleting UserEmployee Role";
        public const string EmployeeRoleUpdatedError = "An Error Occured While Updating UserEmployee Role";
        public const string EmployeeRoleConflict = "UserEmployee Role Already Exists!";
        public const string EmployeeRoleNotFound = "UserEmployee Role Not Found";

        public const string EmployeeRoleSpaceControl = "The Name Field Nannot Contain a Space.";

        public const string ErrorWhenDeleted = "Error When Deleted";

        public const string Name = "name";
        public const string Description = "description";
        public const string DiscountRate = "discountRate";

    }
    public class GroupRoleConstants
    {
        public const string ErrorWhenDeleted = "Error When Deleted";
        public const string KeycloakError = "Keycloak Error";
        public const string UserInRole = "There are active users associated with the permission you wish to delete. Please check the user permissions.";
    }

    public const string NumberSequence = "1234567890";
    public const string IsVerifiedUser = "This user is verified!";
    public const int OtpLenght = 6;
    public const string TurkeyCountryCode = "TR";
    public const SiteStatusEnum SiteStatusOpen = SiteStatusEnum.Open;
    public const SiteStatusEnum SiteStatusClosed = SiteStatusEnum.Closed;
    public const UserStatusEnum UserStatusDeleted = UserStatusEnum.Deleted;
    public const UserStatusEnum UserStatusActive = UserStatusEnum.Active;
    public const UserStatusEnum UserStatusInactive = UserStatusEnum.Inactive;
}