using RescueSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Domain.Entities
{
    public class OtpCode : BaseEntities
    {
        public string Email { get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
        public bool IsUsed { get; set; } = false;
    }
}
