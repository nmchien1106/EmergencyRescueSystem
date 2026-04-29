using System;
using System.Collections.Generic;
using System.Text;
using RescueSystem.Application.DTOs.Media;
using Microsoft.AspNetCore.Http;
using RescueSystem.Application.Common.Enums;
using CloudinaryDotNet.Actions;

namespace RescueSystem.Application.Common.Interfaces.Services
{
    public interface ICloudinaryService
    {
        Task<MediaResult> UploadImageAsync(IFormFile formFile, CancellationToken cancellationToken = default);
        Task<MediaResult> UploadVideoAsync(IFormFile formFile, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string publicId, ResourceType resourceType);
    }
}
