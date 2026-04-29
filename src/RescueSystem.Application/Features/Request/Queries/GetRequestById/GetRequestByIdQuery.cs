using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.DTOs.Request;

namespace RescueSystem.Application.Features.Request.Queries.GetRequestById
{
    public class GetRequestByIdQuery : IRequest<RequestDTO>
    {
        public Guid RequestId { get; set; }
    }
}
