using System.Xml.Serialization;

namespace LiepaLimited.Test.ConsoleApp.Dto
{

    public class UserInfoDto
    {
        public UserInfoDto(int id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        public UserInfoDto()
        {
        }

        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement]
        public string Status { get; set; }
    }
}
