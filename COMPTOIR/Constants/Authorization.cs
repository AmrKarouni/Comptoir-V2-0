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
        public const string default_username = "amr";
        public const string default_email = "amrd@comptoir.com";
        public const string default_password = "Aa@123456";
        public const Roles default_role = Roles.Developer;
        public const Roles user_role = Roles.User;
    }
}
