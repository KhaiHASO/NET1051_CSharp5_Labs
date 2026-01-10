namespace bai4.Areas.Admin.ViewModels;

public class DashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalRoles { get; set; }
    public int UnconfirmedUsers { get; set; } // Demo: Users without email confirmation
    public int NewUsersToday { get; set; } // Demo: Users created today (requires CreatedDate in User model, standard Identity doesn't have it easily accessible without custom column, will mock or check approach)
}
