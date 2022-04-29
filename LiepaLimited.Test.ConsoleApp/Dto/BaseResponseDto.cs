using System.Xml.Serialization;

namespace LiepaLimited.Test.ConsoleApp.Dto
{
    public class BaseResponseDto
    {
        [XmlAttribute]
        public bool Success { get; set; }
    }
}
