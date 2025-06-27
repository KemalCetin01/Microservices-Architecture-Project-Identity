namespace MS.Services.Identity.Application.Constants;
public partial class Constants
{
    public static class KafkaConstants
    {
        public static class Topic
        {
            public const string ErpCurrentAccount = "tpc_erp_currentaccount_changes";
        }

        public static class Key
        {
            public const string ErpCurrentAccountKey = "tpc_erp_currentaccount";
        }
    }
}