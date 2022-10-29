using AutoMapper;
using Lab3.CodeFirst.DB;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.CodeFirst.Services;

public class EventService : IEventService
{
    private readonly DbSet<Event> _events;
    private readonly DbSet<User> _users;
    private readonly DbSet<Group> _groups;
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public EventService(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _events = context.Set<Event>();
        _users = context.Set<User>();
        _groups = context.Set<Group>();
    }


    public async Task<Event?> GetEventById(Guid id)
    {
        return await _events.FirstOrDefaultAsync(e => e.Id == id);
    }

    public IQueryable<Event> GetEvents(PageInfo? pageInfo = null)
    {
        if (pageInfo is null)
        {
            return _events.AsQueryable();
        }

        return _events.Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
            .Take(pageInfo.PageSize);
    }

    public async Task AddEvent(EventDto @event)
    {
        await RequestEvent(@event, RequestType.Add);
    }

    public async Task UpdateEvent(EventDto @event)
    {
        if (@event.Id is null)
        {
            throw new ArgumentException("Event Id is missing.");
        }

        if (!await _events.AnyAsync(e => e.Id == @event.Id))
        {
            throw new ArgumentException($"Event with id {@event.Id} does not exist.");
        }
        await RequestEvent(@event, RequestType.Update);
    }

    public async Task<bool> DeleteEvent(Guid id)
    {
        Event? @event = await GetEventById(id);
        if (@event is null)
        {
            return false;
        }

        _context.Entry(@event).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return true;
    }

    public IQueryable<Event> GetEventsForUser(Guid userId, Position position)
    {
        if (position is Position.Student)
        {
            return _events.Where(e => e.Group.Students.Any(s => s.Id == userId));
        }

        if (position is Position.Assistant or Position.Professor)
        {
            return _events.Where(e => e.Professor.Id == userId);
        }

        throw new ArgumentException("Wrong user type: SystemAdministrator.");
    }

    public IQueryable<Event> GetEventsForGroup(Guid groupId)
    {
        return _events.Where(e => e.Group.Id == groupId);
    }

    public bool ValidateReadRights(Position position)
    {
        return true;
    }

    public bool ValidateWriteRights(Position position)
    {
        return position is Position.SystemAdministrator;
    }

    private async Task RequestEvent(EventDto @event, RequestType type)
    {
        var @new = _mapper.Map<Event>(@event);
        Group? group = await _groups.FirstOrDefaultAsync(g => g.Id == @event.GroupId);
        if (group is null)
        {
            throw new ArgumentException($"Group with id {@event.GroupId} does not exist.");
        }

        User? user = await _users.FirstOrDefaultAsync(u => u.Id == @event.ProfessorId);
        if (user is null || user.Position is Position.Student or Position.SystemAdministrator)
        {
            throw new ArgumentException(
                $"User with id {@event.ProfessorId} either does not exist or does not have proper position.");
        }

        @new.Group = group;
        @new.Professor = user;
        _context.Entry(@new).State = type == RequestType.Add ? EntityState.Added : EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}

