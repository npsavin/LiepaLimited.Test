using System.Xml.Serialization;
using LiepaLimited.Test.Application.Dto;

namespace LiepaLimited.Test.ConsoleApp.Dto
{
    [XmlRoot("Response")]
    public class CreateUserResponseDto: BaseResponseDto
    {
        public CreateUserResponseDto(UserInfoDto user)
        {
            User = user;
            Success = true;
            ErrorId = 0;
        }

        public CreateUserResponseDto()
        {
        }

        [XmlAttribute]
        public int ErrorId { get; set; }


        [XmlElement("user")]
        public UserInfoDto User { get; set; }
    }
}
