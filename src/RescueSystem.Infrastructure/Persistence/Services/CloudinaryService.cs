using System;
using System.Collections.Generic;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.Common.ExternalSettings;
using RescueSystem.Application.Common.Interfaces.Services;
using RescueSystem.Application.DTOs.Media;

namespace RescueSystem.Infrastructure.Persistence.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly ICloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySetting> settings)
        {
            var cfg = settings.Value;
            var account = new Account(cfg.CloudName, cfg.ApiKey, cfg.ApiSecret);
            _cloudinary = new Cloudinary(account) { Api = { Secure = true } };
        }

        public async Task<MediaResult> UploadImageAsync(IFormFile formFile, CancellationToken cancellationToken = default)
        {
            await using var stream = formFile.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(formFile.FileName, stream),
                Folder = "rescue-system/images",
                UseFilename = true,
                Overwrite = false,
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);
            if (uploadResult.Error != null)
            {
                throw new InternalServerErrorException(message: uploadResult.Error?.ToString() ?? "Xảy ra lỗi khi upload hình ảnh");
            }
            return MapToResult(uploadResult, "image");
        }

        public async Task<MediaResult> UploadVideoAsync(IFormFile formFile, CancellationToken cancellationToken = default)
        {
            await using var stream = formFile.OpenReadStream();
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(formFile.FileName, stream),
                Folder = "rescue-system/videos",
                UseFilename = true,
                Overwrite = false,
                EagerTransforms = new List<Transformation>
                {
                    new Transformation().Quality("auto").FetchFormat("mp4")
                },
                EagerAsync = true,

            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);
            if (uploadResult.Error != null)
            {
                throw new InternalServerErrorException(message: uploadResult.Error?.ToString() ?? "Xảy ra lỗi khi upload video");
            }
            return MapToResult(uploadResult, "video");
        }
        public async Task<bool> DeleteAsync(string publicId, ResourceType resourceType)
        {
            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = resourceType
            };
            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
            return deleteResult.Result == "ok";
        }
        public static MediaResult MapToResult(RawUploadResult raw, string resourceType)
        {
            return new MediaResult
            {
                PublicId = raw.PublicId,
                Url = raw.Url?.ToString() ?? string.Empty,
                SecureUrl = raw.SecureUrl?.ToString() ?? string.Empty,
                Format = raw.Format,
                Bytes = raw.Bytes,
                ResourceType = resourceType
            };
        }

    }
}
