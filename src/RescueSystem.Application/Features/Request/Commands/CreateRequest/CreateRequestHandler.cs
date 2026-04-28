using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Domain.Entities;

namespace RescueSystem.Application.Features.Request.Commands.CreateRequest
{
    public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, RescueRequest>
    {
        IRequestRespository _requestRepository;
        public CreateRequestHandler(IRequestRespository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        public async Task<RescueRequest> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var RequestId = Guid.NewGuid();
            var RescueReq = new RescueRequest
            {
                Id = RequestId,
                UserId = request.UserId,
                EmergencyType = request.EmergencyType,
                Status = request.Status,
                LocationId = request.LocationId,
                Description = request.Description,
                Medias = request.Media?.Select(m => new RequestMedia
                {
                    PublicId = m.PublicId,
                    SescueUrl = m.SescueUrl,
                    ResourceType = m.ResourceType,
                    RequestId = RequestId
                }
                ).ToList() ?? new List<RequestMedia>()
            };

            await _requestRepository.CreateAsync(RescueReq);

            return RescueReq;
        }

    }
}
