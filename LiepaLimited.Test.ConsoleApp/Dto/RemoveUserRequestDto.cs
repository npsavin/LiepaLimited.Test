namespace LiepaLimited.Test.ConsoleApp.Dto
{
    public class RemoveUserRequestDto
    {
        public RemoveUserRequestDto(int id)
        {
            RemoveUser = new RemoveUserDto(id);
        }

        public RemoveUserRequestDto()
        {
        }

        public RemoveUserDto RemoveUser { get; set; }
    }
}
