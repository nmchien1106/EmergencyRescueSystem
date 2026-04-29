using System;
using System.Collections.Generic;
using System.Text;
using RescueSystem.Domain.Entities.Base;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Domain.Entities
{
    public class RequestMedia : BaseEntities
    {
        public Guid RequestId { get; set; }
        public string SecureUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public MediaType ResourceType { get; set; } = MediaType.Image;
        public RescueRequest? Request { get; set; }
    }
}
