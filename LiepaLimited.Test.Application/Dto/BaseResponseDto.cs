using System.Xml.Serialization;

namespace LiepaLimited.Test.Application.Dto
{
    public class BaseResponseDto
    {
        [XmlAttribute]
        public bool Success { get; set; }
    }
}
