using System.Xml.Serialization;

namespace LiepaLimited.Test.Application.Dto
{

    public class UserInfoDto
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement]
        public string Status { get; set; }
    }
}
