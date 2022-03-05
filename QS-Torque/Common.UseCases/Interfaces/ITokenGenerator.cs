namespace Common.UseCases.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(AuthenticationUser user, string pcfqdn);
    }

    public class AuthenticationUser
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
