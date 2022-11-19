namespace MembershipSystem.Strategy.Models
{
    public class Setting
    {
        public const string ClaimDatabaseType = "databasetype";
        public EDatabaseType DatabaseType;
        public EDatabaseType GetDefaultDatabaseType => EDatabaseType.SqlServer;
    }
}
