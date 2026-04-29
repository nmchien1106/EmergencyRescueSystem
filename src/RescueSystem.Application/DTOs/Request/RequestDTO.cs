using System;
using System.Collections.Generic;
using System.Text;
using RescueSystem.Application.DTOs.Base;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.DTOs.Request
{
    public class RequestDTO : BaseDTO
    {

        public UserDTO? RequestedBy { get; set; }

        public EmergencyType EmergencyType { get; set; }

        public Priority Priority { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.PENDING;

        public Location? Location { get; set; }

        public string Description { get; set; } = string.Empty;
        public List<RequestMediaDTO> Medias { get; set; } = new List<RequestMediaDTO>();
    }

    public class NonRelationRequestDTO : BaseDTO {
        public EmergencyType EmergencyType { get; set; }

        public Priority Priority { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.PENDING;
        public Location? Location { get; set; }

    }
}
