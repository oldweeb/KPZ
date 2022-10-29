using AutoMapper;
using Lab3.CodeFirst.DB;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.CodeFirst.Services;

public class GroupService : IGroupService
{
    private readonly ScheduleDbContext _context;
    private readonly DbSet<Group> _groups;
    private readonly IMapper _mapper;

    public GroupService(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _groups = context.Set<Group>();
        _mapper = mapper;
    }

    public bool ValidateReadRights(Position position)
    {
        return position == Position.SystemAdministrator;
    }

    public bool ValidateWriteRights(Position position)
    {
        return position == Position.SystemAdministrator;
    }

    public async Task<Group?> GetGroupById(Guid id)
    {
        return await _groups.FirstOrDefaultAsync(g => g.Id == id);
    }

    public IQueryable<Group> GetGroups(PageInfo? pageInfo = null)
    {
        if (pageInfo is null)
        {
            return _groups.AsQueryable();
        }

        return _groups.Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
            .Take(pageInfo.PageSize);
    }

    public async Task AddGroup(GroupDto group)
    {
        await RequestGroup(group, RequestType.Add);
    }

    public async Task UpdateGroup(GroupDto group)
    {
        if (group.Id is null)
        {
            throw new ArgumentException("Group Id is missing.");
        }

        if (!await _groups.AnyAsync(e => e.Id == group.Id))
        {
            throw new ArgumentException($"Event with id {group.Id} does not exist.");
        }
        await RequestGroup(group, RequestType.Update);
    }

    public async Task<bool> DeleteGroup(Guid id)
    {
        Group? group = await GetGroupById(id);
        if (group is null)
        {
            return false;
        }

        _context.Entry(group).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task RequestGroup(GroupDto group, RequestType type)
    {
        if (await _groups.AnyAsync(g => g.Name == group.Name))
        {
            throw new ArgumentException($"Group {group.Name} already exists.");
        }

        var @new = _mapper.Map<Group>(group);
        _context.Entry(@new).State = type == RequestType.Add ? EntityState.Added : EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}