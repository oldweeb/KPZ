using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;

namespace Lab3.CodeFirst.Services;

public interface IEventService : IRightsValidator
{
    Task<Event?> GetEventById(Guid id);
    IQueryable<Event> GetEvents(PageInfo? pageInfo = null);
    Task AddEvent(EventDto @event);
    Task UpdateEvent(EventDto @event);
    Task<bool> DeleteEvent(Guid id);
    IQueryable<Event> GetEventsForUser(Guid userId, Position position);
    IQueryable<Event> GetEventsForGroup(Guid groupId);
}

public enum RequestType
{
    Add,
    Update
}