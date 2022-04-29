using System.Xml.Serialization;

namespace LiepaLimited.Test.Application.Dto
{
    [XmlRoot("Request")]
    public class CreateUserRequestDto
    {
        [XmlElement("user")]
        public UserInfoDto User { get; set; }
    }
}
