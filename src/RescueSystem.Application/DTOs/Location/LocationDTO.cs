using RescueSystem.Application.DTOs.Base;

namespace RescueSystem.Application.DTOs.Location
{
    public class LocationDTO : BaseDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Landmark { get; set; } = string.Empty;
    }
}
