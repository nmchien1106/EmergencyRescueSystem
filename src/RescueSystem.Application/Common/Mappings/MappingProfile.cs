using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RescueSystem.Application.DTOs.Request;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Domain.Entities;

namespace RescueSystem.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping for Request entity
        CreateMap<ApplicationUser, UserDTO>();
        CreateMap<RequestMedia, RequestMediaDTO>();
        CreateMap<RescueRequest, RequestDTO>();
        CreateMap<RescueRequest, NonRelationRequestDTO>();

    }
}
