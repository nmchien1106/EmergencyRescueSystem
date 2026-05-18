using Microsoft.EntityFrameworkCore;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Infrastructure.Persistence.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            //EDIT: DIEU 18/05/2026 - Thêm AsNoTracking để tránh tracking entity khi chỉ cần đọc dữ liệu
            return await _context.Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Contact>> GetByUserIdAsync(Guid userId)
        {
            //EDIT: DIEU 18/05/2026 - Thêm AsNoTracking để tránh tracking entity khi chỉ cần đọc dữ liệu
            return await _context.Contacts
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task CreateAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
