using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LiepaLimited.Test.Application.Dto
{
    public class BaseResponseDto
    {
        [XmlAttribute]
        public bool Success { get; set; }
    }
}
