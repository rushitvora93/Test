namespace UnlockTool.Entities
{
    public class UnlockRequest
    {
        public ulong? UnlockRequestVersion { get; set; }
        public string QSTVersion { get; set; }
        public string Name { get; set; }
        public string Phonenumber { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Windows { get; set; }
        public string PCName { get; set; }
        public string FQDN { get; set; }
        public string LogedinUserName { get; set; }
        public string CurrentLocalDateTime { get; set; }
        public string CurrentUtcDateTime { get; set; }
        public bool HashOk { get; set; } = false;
    }
}
