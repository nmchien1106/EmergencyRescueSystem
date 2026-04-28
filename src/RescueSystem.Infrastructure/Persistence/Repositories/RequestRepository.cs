using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;
using RescueSystem.Infrastructure.Data;

namespace RescueSystem.Infrastructure.Persistence.Repositories
{
    public class RequestRepository : IRequestRespository
    {
        private readonly ApplicationDbContext _context;
        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<RescueRequest>> GetAllAsync()
        {
            return _context.Requests.ToListAsync();
        }
        public Task<RescueRequest?> GetByIdAsync(Guid id)
        {
            return _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
        }
        public Task<List<RescueRequest>> GetByUserIdAsync(Guid userId)
        {
            return _context.Requests.Where(r => r.UserId == userId).ToListAsync();
        }
        public Task<List<RescueRequest>> GetByStatusAsync(RequestStatus status)
        {
            return _context.Requests.Where(r => r.Status == status).ToListAsync();
        }

        public async Task CreateAsync(RescueRequest request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(RescueRequest request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStatusAsync(Guid requestId, RequestStatus status)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (request != null)
            {
                request.Status = status;
                _context.Requests.Update(request);
                await _context.SaveChangesAsync();
                return;
            }
            throw new BadRequestException("Yêu cầu cứu hộ không hợp lệ");

        }
        public async Task DeleteAsync(RescueRequest request)
        {
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}
