using Core.Entities;

namespace State
{
    public static class SessionInformation
    {
        public static string ServerName { get; set; }
        public static User CurrentUser { get; set; }
    }
}
