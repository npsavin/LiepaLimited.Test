namespace LiepaLimited.Test.ConsoleApp.Dto
{
    public class RemoveUserDto
    {
        public RemoveUserDto(int id)
        {
            Id = id;
        }

        public RemoveUserDto()
        {
        }

        public int Id { get; set; }
    }
}
