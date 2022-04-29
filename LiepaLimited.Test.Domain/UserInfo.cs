namespace LiepaLimited.Test.Domain
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
    }

    public enum StatusEnum
    {
        New,
        Active,
        Blocked,
        Deleted
    }
}
