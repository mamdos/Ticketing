namespace Data.Entities.User.Aggregate;

public static class UserRoles
{
    public const string UserAdmin = "UserAdmin";
    public const string TicketAdmin = "TicketAdmin";
    public const string TicketUser = "TicketUser";
    public const string CategoryAdmin = "CategoryAdmin";

    public static IEnumerable<string> GetAll() => 
        new string[] {
            UserAdmin,
            TicketUser,
            TicketAdmin,
            CategoryAdmin
        };
}
