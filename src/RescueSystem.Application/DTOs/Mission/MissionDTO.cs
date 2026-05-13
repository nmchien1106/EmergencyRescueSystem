using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Mission
{
    public class MissionDTO
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string? Description { get; set; }
        public Guid DispatcherId { get; set; }
        public string? DispatcherName { get; set; }
        public Guid RescueTeamId { get; set; }
        public string? TeamName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Status { get; set; }
    }
}

//Note: Sao lại để mấy cái name name ở đây hè, có thể lấy từ db rồi map vào chứ để thế này hơi k ổn, nếu để thế này thì phải đảm bảo lúc tạo mission phải lấy tên dispatcher và team rồi gán vào, nếu sau này có đổi tên thì lại phải update lại mấy cái mission liên quan nữa, nên tốt nhất là bỏ mấy cái name đi chỉ giữ id thôi, còn muốn lấy tên thì join với bảng user và rescue team rồi map vào DTO là được