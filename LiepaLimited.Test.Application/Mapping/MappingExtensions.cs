using System;
using LiepaLimited.Test.Application.Exceptions;
using LiepaLimited.Test.Domain;

namespace LiepaLimited.Test.Application.Mapping
{
    public static class MappingExtensions
    {
        public static StatusEnum MapToStatus(this string status)
        {
            if (!Enum.TryParse(status, out StatusEnum statusEnum))
            {
                throw new BadRequestException($"User status {status} is invalid");
            }

            return statusEnum;
        }
    }
}
