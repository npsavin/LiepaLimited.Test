using LiepaLimited.Test.ConsoleApp.Dto;
using Newtonsoft.Json;

namespace LiepaLimited.Test.Application.Dto
{
    public class RemoveUserResponseDto: BaseResponseDto
    {
        public RemoveUserResponseDto(UserInfoDto user)
        {
            Success = true;
            Message = "User was removed";
            User = user;
        }

        [JsonProperty("Msg")]
        public string Message { get; set; }
        
        [JsonProperty("user")]
        public UserInfoDto User { get; set; }
    }
}
