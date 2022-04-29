using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace LiepaLimited.Test.Application.Dto
{
    [XmlRoot("Response")]
    public class ErrorResponseDto: BaseResponseDto
    {
        public ErrorResponseDto(int errorId, string message)
        {
            Success = false;
            ErrorId = errorId;
            Message = message;
        }

        public ErrorResponseDto()
        {
        }

    
        [XmlAttribute]
        public int ErrorId { get; set; }

        [XmlElement("ErrorMsg")]
        [JsonPropertyName("Msg")]
        public string Message { get; set; }

    }
}
