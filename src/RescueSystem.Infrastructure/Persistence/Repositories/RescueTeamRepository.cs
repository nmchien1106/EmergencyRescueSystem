using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Application.Common.Exception;

namespace RescueSystem.Infrastructure.Persistence.Repositories{
    public class RescueTeamRepository : IRescueTeamRepository
    {
        private readonly ApplicationDbContext _context;
        public RescueTeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(RescueTeam rescueTeam)
        {
            await _context.RescueTeams.AddAsync(rescueTeam);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<List<RescueTeam>> GetAllAsync()
        {
            return await _context.RescueTeams
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<RescueTeam?> GetByIdAsync(Guid id)
        {
            return await _context.RescueTeams
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> RemoveMemberAsync(Guid teamId, Guid memberId) {
            var team = await _context.RescueTeams
                .Include(t=>t.Members)
                .FirstOrDefaultAsync(t=>t.Id == teamId);
            if(team==null) {
                throw new NotFoundException("Rescue team not found");
            }

            var member = team.Members.FirstOrDefault(member=>member.Id == memberId);
            if(member==null){
                return false;
            }

            team.Members.Remove(member);
            team.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddMemberAsync(Guid teamId, Guid memberId) {
            var team = await _context.RescueTeams
                .Include(t=>t.Members)
                .FirstOrDefaultAsync(t=>t.Id == teamId);
            
            if(team==null) {
                throw new NotFoundException("Rescue team not found");
            }

            var member = await _context.Users.FindAsync(memberId);

            if(member==null){
                throw new NotFoundException("User not found");
            }

            if(team.Members.Any(m=>m.Id == memberId)){
                    throw new BadRequestException("User is already a member of the rescue team");
            }

            team.Members.Add(member);
            team.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<List<ApplicationUser>> GetMembersByTeamIdAsync(Guid teamId) {
            var team = await _context.RescueTeams
                .Include(t=>t.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(t=>t.Id == teamId);
            
            if(team==null) {
                throw new NotFoundException("Rescue team not found");
            }

            return team.Members.ToList();
        }

        public async Task<bool> UpdateTeamStatusAsync(Guid teamId, TeamStatus newStatus) {
            var team = await _context.RescueTeams.FindAsync(teamId);

            if(team==null) {
                throw new NotFoundException("Rescue team not found");
            }
            
            team.Status = newStatus;
            team.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(RescueTeam rescueTeam)
        {
            _context.RescueTeams.Remove(rescueTeam);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Mission>> GetMissionsByTeamIdAsync(Guid teamId)
        {
            var team = await _context.RescueTeams
                .AsNoTracking()
                .AnyAsync(t => t.Id == teamId);

            if (!team)
            {
                throw new NotFoundException("Rescue team not found");
            }
             return await _context.Missions
        .AsNoTracking()
        .Include(m => m.Request)
        .Include(m => m.Dispatcher)
        .Include(m => m.RescueTeam)
        .Where(m => m.RescueTeamId == teamId)
        .OrderByDescending(m => m.StartTime)
        .ToListAsync();
        }
        // public async Task<List<RescueTeam>> GetByStatusAsync(TeamStatus status)
        // {
        //     return await _context.RescueTeams.Where(x => x.Status == status).ToListAsync();
        // }
        // public async Task<bool> CreateAsync(RescueTeam rescueTeam)
        // {
        //     await _context.RescueTeams.AddAsync(rescueTeam);
        //     return await _context.SaveChangesAsync() > 0;
        // }
        // public async Task<bool> UpdateAsync(RescueTeam rescueTeam)
        // {
        //     var existingTeam = await _context.RescueTeams.FirstOrDefaultAsync(x => x.Id == rescueTeam.Id);
        //     if (existingTeam == null)
        //     {
        //         throw new NotFoundException("Rescue team not found");
        //     } 
        //     existingTeam.TeamName = rescueTeam.TeamName;
        //     existingTeam.TeamLeaderId = rescueTeam.TeamLeaderId;
        //     existingTeam.BaseLocationId = rescueTeam.BaseLocationId;
        //     existingTeam.Status = rescueTeam.Status;
        //     existingTeam.UpdatedAt = DateTime.UtcNow;
        //     _context.RescueTeams.Update(rescueTeam);
        //     return await _context.SaveChangesAsync() > 0;
        // }
        // public async Task<bool> DeleteAsync(RescueTeam rescueTeam)
        // {
        //     _context.RescueTeams.Remove(rescueTeam);
        //     return await _context.SaveChangesAsync() > 0;
        // }
    }
}