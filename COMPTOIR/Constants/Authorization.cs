namespace COMPTOIR.Constants
{
    public class Authorization
    {
        public enum Roles
        { 
            SuperUser,
            Developer,
            InventoryAdmin,
            InventoryUser,
            POSAdmin,
            POSUser,
            ProductionAdmin,
            ProductionUser,
            User,
        }
        public const string default_username = "Amr";
        public const string default_email = "amrkarouni@gmail.com";
        public const string default_password = "Aa@123456";
        public const Roles default_role = Roles.Developer;
        public const Roles user_role = Roles.User;
    }
}
