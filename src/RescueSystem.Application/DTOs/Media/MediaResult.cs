using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Media
{
    public class MediaResult
    {
        public string PublicId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string SecureUrl { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public long Bytes { get; set; }
        public string ResourceType { get; set; } = string.Empty; // "image" | "video"
    }
}
