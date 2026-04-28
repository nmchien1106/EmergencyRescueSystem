using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.Common.Interfaces.Repositories
{
    public interface IRequestRespository
    {
        Task<List<RescueRequest>> GetAllAsync();
        Task<RescueRequest?> GetByIdAsync(Guid id);
        Task<List<RescueRequest>> GetByUserIdAsync(Guid userId);
        Task<List<RescueRequest>> GetByStatusAsync(RequestStatus status);

        Task CreateAsync(RescueRequest request);
        Task UpdateAsync(RescueRequest request);
        Task UpdateStatusAsync(Guid requestId, RequestStatus status);
        Task DeleteAsync(RescueRequest request);
    }
}
