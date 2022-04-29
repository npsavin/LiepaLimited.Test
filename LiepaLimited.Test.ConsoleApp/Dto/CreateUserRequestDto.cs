using System.Xml.Serialization;

namespace LiepaLimited.Test.ConsoleApp.Dto
{
    [XmlRoot("Request")]
    public class CreateUserRequestDto
    {
        public CreateUserRequestDto(int id, string name, string status)
        {
            User = new UserInfoDto(id, name, status);
        }

        public CreateUserRequestDto()
        {
        }

        [XmlElement("user")]
        public UserInfoDto User { get; set; }
    }
}
